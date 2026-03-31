// Copyright (c) 2014-2020 QUIKSharp Authors. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using QuikSharp;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesignPrototype
{
    public partial class MainWindow : Window
    {
        private Quik _quik;
        private bool _isConnected;
        private string _clientCode;
        private readonly int _quikPort;
        private readonly DispatcherTimer _uptimeTimer;
        private DateTime _connectionStartTime;
        private bool _isProcessing;

        public MainWindow()
        {
            InitializeComponent();

            _quikPort = int.TryParse(ConfigurationManager.AppSettings["QuikPort"], out var port)
                ? port
                : 34130;

            _uptimeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _uptimeTimer.Tick += (s, e) =>
            {
                if (_isConnected)
                {
                    TxtUptime.Text = (DateTime.Now - _connectionStartTime).ToString(@"hh\:mm\:ss");
                }
            };

            MainFrame.Navigate(new Pages.HomePage());
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageTag)
            {
                switch (pageTag)
                {
                    case "Home":
                        MainFrame.Navigate(new Pages.HomePage());
                        break;
                }
            }
        }

        private void UpdateStatus(string message, Brush color)
        {
            StatusText.Text = message;
            StatusText.Foreground = color;
            StatusIndicator.Fill = color;
        }
    }
}