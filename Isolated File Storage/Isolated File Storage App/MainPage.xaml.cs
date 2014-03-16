using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Isolated_File_Storage_App.Resources;
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;

namespace Isolated_File_Storage_App
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            string text = "Book Name: " + BookName.Text + "\nBook Author: " + BookAuthor.Text + "\nBook Description: " + BookDesc.Text;
            SavedBookName.Text = text;
            using (IsolatedStorageFile wpassignmentfile = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (wpassignmentfile.FileExists("wpassignmentfile.txt"))
                { 
                    //Write details to file
                    using (IsolatedStorageFileStream ars = wpassignmentfile.OpenFile("wpassignmentfile.txt", FileMode.Append))
                    {
                        StreamWriter writer = new StreamWriter(ars);
                        writer.Write(writer.NewLine + text);
                        writer.Close();
                        //MessageBox.Show("Appeneded");
                    }
                }
                else
                {
                    //Create file
                    using (IsolatedStorageFileStream rawStream = wpassignmentfile.CreateFile("wpassignmentfile.txt"))
                    {
                        StreamWriter writer = new StreamWriter(rawStream);
                        writer.Write(text);
                        writer.Close();
                        //MessageBox.Show("Created");
                    }
                }
            }
        }

        //Clear text on Clear button click
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            BookName.Text = String.Empty;
            BookAuthor.Text = String.Empty;
            BookDesc.Text = String.Empty;
            SavedBookName.Text = "";
        }

        //Empty TextBox on focus
        public void BookName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox BN = (TextBox)sender;
            BN.Text = string.Empty;
            BN.GotFocus -= BookName_GotFocus;
        }

        public void BookAuthor_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox BA = (TextBox)sender;
            BA.Text = string.Empty;
            BA.GotFocus -= BookAuthor_GotFocus;
        }

        public void BookDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox BD = (TextBox)sender;
            BD.Text = string.Empty;
            BD.GotFocus -= BookDesc_GotFocus;
        }

    }
}