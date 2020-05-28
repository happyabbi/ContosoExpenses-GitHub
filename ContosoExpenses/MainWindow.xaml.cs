﻿// ******************************************************************

// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.

// ******************************************************************

using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ContosoExpenses.Models;
using ContosoExpenses.Services;
using Microsoft.AppCenter.Analytics;

namespace ContosoExpenses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DesktopBridge.Helpers helpers = new DesktopBridge.Helpers();
            //if (helpers.IsRunningAsUwp())
            //{
            //    string version = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
            //    this.Title = $"Real Estate - Version {version}";
            //}

            LoadData();
        }

        private void LoadData()
        {
            DatabaseService db = new DatabaseService();
            db.InitializeDatabase();

            CustomersGrid.ItemsSource = db.GetEmployees();
        }

        private void OnSelectedEmployee(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var employee = e.AddedItems[0] as Employee;
                if (employee != null && employee.EmployeeId != 0)
                {
                    Analytics.TrackEvent("Employee selected", new Dictionary<string, string> { { "Name", employee.FirstName }, { "Surname", employee.LastName }, { "Id", employee.EmployeeId.ToString() } });

                    ExpensesList detail = new ExpensesList();
                    detail.EmployeeId = employee.EmployeeId;
                    detail.Show();
                }
            }
        }

        private void OnOpenAbout(object sender, RoutedEventArgs e)
        {
            AboutView about = new AboutView();
            about.ShowDialog();
        }
    }
}
