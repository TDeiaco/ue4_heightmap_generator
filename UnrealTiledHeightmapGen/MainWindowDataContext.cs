using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using UnrealTiledHeightmapGen.Annotations;
using MessageBox = System.Windows.MessageBox;

namespace UnrealTiledHeightmapGen
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {

        private int _horizontalResolution = 512;
        private int _verticalResolution = 512;
        private int _horizontalTileCount = 5;
        private int _verticalTileCount = 5;
        private List<ResolutionComboBoxItem> _resolutionComboBoxItems;
        private ResolutionComboBoxItem _selectedResolutionComboBoxItem;
        private string _heightmapFilenames = "TiledHeightmap";
        private string _baseDirectory = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\Documents\\";

        public int HorizontalResolution
        {
            get { return _horizontalResolution; }
            set
            {
                if (value == _horizontalResolution) return;
                _horizontalResolution = value;
                OnPropertyChanged();
            }
        }

        public int VerticalResolution
        {
            get { return _verticalResolution; }
            set
            {
                if (value == _verticalResolution) return;
                _verticalResolution = value;
                OnPropertyChanged();
            }
        }

        public int HorizontalTileCount
        {
            get { return _horizontalTileCount; }
            set
            {
                if (value == _horizontalTileCount) return;
                _horizontalTileCount = value;
                OnPropertyChanged();
            }
        }

        public int VerticalTileCount
        {
            get { return _verticalTileCount; }
            set
            {
                if (value == _verticalTileCount) return;
                _verticalTileCount = value;
                OnPropertyChanged();
            }
        }

        public List<ResolutionComboBoxItem> ResolutionComboBoxItems
        {
            get { return _resolutionComboBoxItems; }
            set
            {
                if (Equals(value, _resolutionComboBoxItems)) return;
                _resolutionComboBoxItems = value;
                OnPropertyChanged();
            }
        }

        public ResolutionComboBoxItem SelectedResolutionComboBoxItem
        {
            get { return _selectedResolutionComboBoxItem; }
            set
            {
                if (Equals(value, _selectedResolutionComboBoxItem)) return;
                _selectedResolutionComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public string HeightmapFilenames
        {
            get { return _heightmapFilenames; }
            set
            {
                if (value == _heightmapFilenames) return;
                _heightmapFilenames = value;
                OnPropertyChanged();
            }
        }

        public string BaseDirectory
        {
            get { return _baseDirectory; }
            set
            {
                if (value == _baseDirectory) return;
                _baseDirectory = value;
                OnPropertyChanged();
            }
        }

        public ICommand GenerateCommand => new RelayCommand(Generate);
        public ICommand BrowseCommand => new RelayCommand(Browse);

        public MainWindowDataContext()
        {
            ResolutionComboBoxItems = new List<ResolutionComboBoxItem>
            {
                new ResolutionComboBoxItem {Resolution = "127x127", Id =127},
                new ResolutionComboBoxItem {Resolution = "253x253", Id =253},
                new ResolutionComboBoxItem {Resolution = "255x255", Id =255},
                new ResolutionComboBoxItem {Resolution = "505x505", Id =505},
                new ResolutionComboBoxItem {Resolution = "509x509", Id =509},
                new ResolutionComboBoxItem {Resolution = "1009x1009", Id =1009},
                new ResolutionComboBoxItem {Resolution = "2017x2017", Id =2017},
                new ResolutionComboBoxItem {Resolution = "4033x4033", Id =4033},
                new ResolutionComboBoxItem {Resolution = "8161x8061", Id =8161}
            };

            SelectedResolutionComboBoxItem = new ResolutionComboBoxItem {Id = 505};
        }

        private void Generate(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(HeightmapFilenames))
                {
                    MessageBox.Show("Heightmap filenames required!", "Error", MessageBoxButton.OK);
                    return;
                }

                try
                {
                    if (!Directory.Exists(BaseDirectory))
                    {
                        Directory.CreateDirectory(BaseDirectory);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error creating directory!", MessageBoxButton.OK);
                }

                for (int xTileIndex = 0; xTileIndex < SelectedResolutionComboBoxItem.Id; xTileIndex++)
                {
                    for (int yTileIndex = 0; yTileIndex < SelectedResolutionComboBoxItem.Id; yTileIndex++)
                    {
                        var filename = $"{BaseDirectory}\\{HeightmapFilenames}_X{xTileIndex}_Y{yTileIndex}.raw";

                        var writeStream = new FileStream(filename, FileMode.OpenOrCreate);
                        BinaryWriter writeBinay = new BinaryWriter(writeStream);
                        for (int x = 0; x < HorizontalResolution + 1; x++)
                        {
                            for (int y = 0; y < VerticalResolution + 1; y++)
                            {
                                writeBinay.Write(new byte[] { 0, 0 });
                            }
                        }

                        writeBinay.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void Browse(object obj)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    BaseDirectory = fbd.SelectedPath;
                }
            }
        }


        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
