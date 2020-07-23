using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace ContactManager
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<Contact> contacts;
        public MainPage()
        {
            InitializeComponent();
            GetXML();
        }

        private void GetXML()
        {
            AssetManager assets = Android.App.Application.Context.Assets;
            var filename = assets.Open("ContactManager.xml");
                
            using (var reader = new StreamReader(filename))
            {
                var serializer = new XmlSerializer(typeof(List<Contact>),
                new XmlRootAttribute("Contact"));
                contacts = (List<Contact>)serializer.Deserialize(reader);
            }
            pckID.ItemsSource = contacts;
        }

        private void pckID_selected(object sender, EventArgs e)
        //                SelectedIndexChanged="PickerItems_selected" 
        {
            try
            {
                entFirstName.Text = contacts[pckID.SelectedIndex].FirstName;
                entLastName.Text = contacts[pckID.SelectedIndex].LastName;
                entMobile.Text = contacts[pckID.SelectedIndex].Mobile;
                entEmail.Text = contacts[pckID.SelectedIndex].Email;
            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }
        private void BtnModify_clicked(object sender, EventArgs e)
        {
            contacts[pckID.SelectedIndex].FirstName = entFirstName.Text;
            contacts[pckID.SelectedIndex].LastName = entLastName.Text;
            contacts[pckID.SelectedIndex].Mobile = entMobile.Text;
            contacts[pckID.SelectedIndex].Email = entEmail.Text;

            AssetManager assets = Android.App.Application.Context.Assets;
            var filename = assets.Open("ContactsManager.xml");
            var writableFile = Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal), "ContactManager.xml");
            
            if (!File.Exists(writableFile))
            {
                var fileStream = File.OpenWrite(writableFile);
                filename.CopyTo(fileStream);
                fileStream.Dispose();
            }

            using (StreamWriter writer = new StreamWriter(writableFile))
            {
                var serializer = new XmlSerializer(typeof(List<Contact>),
                    new XmlRootAttribute("ContactManager"));
                serializer.Serialize(writer, contacts);
            }
        }
        private void BtnDelete_clicked(object sender, EventArgs e)
        {
            contacts.RemoveAt(pckID.SelectedIndex);
            pckID.ItemsSource = null;
            pckID.ItemsSource = contacts;

            entFirstName.Text = "";
            entLastName.Text = "";
            entMobile.Text = "";
            entEmail.Text = "";

            AssetManager assets = Android.App.Application.Context.Assets;
            var filename = assets.Open("ContactsManager.xml");
            var writableFile = Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal), "ContactManager.xml");

            if (!File.Exists(writableFile))
            {
                var fileStream = File.OpenWrite(writableFile);
                filename.CopyTo(fileStream);
                fileStream.Dispose();
            }

            using (StreamWriter writer = new StreamWriter(writableFile))
            {
                var serializer = new XmlSerializer(typeof(List<Contact>),
                    new XmlRootAttribute("ContactManager"));
                serializer.Serialize(writer, contacts);
            }
        }
    }
}

