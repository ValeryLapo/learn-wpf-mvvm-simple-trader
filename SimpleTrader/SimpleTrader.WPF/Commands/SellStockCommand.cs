﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _viewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;
        public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;

            _viewModel.PropertyChanged += BuyViewModelOnPropertyChanged;
        }

        private void BuyViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SellViewModel.CanSellStock))
            {
                OnCanExecuteChanged();
            }
        }
        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanSellStock && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.StatusMessage = string.Empty;
            _viewModel.ErrorMessage = string.Empty;

            try
            {
                string symbol = _viewModel.Symbol;
                int shares = _viewModel.SharesToSell;

                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);
                _accountStore.CurrentAccount = account;

                _viewModel.StatusMessage = $"Successfully sold {shares} shares of {symbol}";
            }
            catch (InsufficientSharesException ex)
            {
                _viewModel.ErrorMessage = $"Account has insufficient shares. Please transfer more money into your account. You only have {ex.RequiredShares} shares.";
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
