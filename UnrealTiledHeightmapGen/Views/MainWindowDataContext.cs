using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using UnrealTiledHeightmapGen.Core;
using UnrealTiledHeightmapGen.Data;
using UnrealTiledHeightmapGen.Properties;
using MessageBox = System.Windows.MessageBox;

namespace UnrealTiledHeightmapGen.Views
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        private ObservableCollection<ComboBoxItem> _imageOutputTypes;
        public ObservableCollection<ComboBoxItem> ImageOutputTypes
        {
            get { return _imageOutputTypes; }
            set
            {
                if (Equals(value, _imageOutputTypes)) return;
                _imageOutputTypes = value;
                OnPropertyChanged();
            }
        }

        private ComboBoxItem _selectedImageOutputType;
        public ComboBoxItem SelectedImageOutputType
        {
            get { return _selectedImageOutputType; }
            set
            {
                if (Equals(value, _selectedImageOutputType)) return;
                _selectedImageOutputType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GenerationHeightComboBoxItem> _generationHeightComboBoxItems;
        public ObservableCollection<GenerationHeightComboBoxItem> GenerationHeightComboBoxItems
        {
            get { return _generationHeightComboBoxItems; }
            set
            {
                if (Equals(value, _generationHeightComboBoxItems)) return;
                _generationHeightComboBoxItems = value;
                OnPropertyChanged();
            }
        }

        private GenerationHeightComboBoxItem _selectedGenerationHeightComboBoxItem;
        public GenerationHeightComboBoxItem SelectedGenerationHeightComboBoxItem
        {
            get { return _selectedGenerationHeightComboBoxItem; }
            set
            {
                if (Equals(value, _selectedGenerationHeightComboBoxItem)) return;
                _selectedGenerationHeightComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        private List<ResolutionComboBoxItem> _resolutionComboBoxItems;
        private ResolutionComboBoxItem _selectedResolutionComboBoxItem;

        private List<TileCountComboBoxItem> _horizontalTileCountComboBoxItems;
        private TileCountComboBoxItem _selectedHorizontalTileCountComboBoxItem;

        private List<TileCountComboBoxItem> _verticalTileCountComboBoxItems;
        private TileCountComboBoxItem _selectedVerticalTileCountComboBoxItem;

        private string _heightmapStartingElevation;

        private string _heightmapFilenames = "TiledHeightmap";
        private string _baseDirectory = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\Documents";

        private bool _generateButtonEnabled = false;

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

        public List<TileCountComboBoxItem> HorizontalTileCountComboBoxItems
        {
            get { return _horizontalTileCountComboBoxItems; }
            set
            {
                if (Equals(value, _horizontalTileCountComboBoxItems)) return;
                _horizontalTileCountComboBoxItems = value;
                OnPropertyChanged();
            }
        }

        public TileCountComboBoxItem SelectedHorizontalTileCountComboBoxItem
        {
            get { return _selectedHorizontalTileCountComboBoxItem; }
            set
            {
                if (Equals(value, _selectedHorizontalTileCountComboBoxItem)) return;
                _selectedHorizontalTileCountComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public List<TileCountComboBoxItem> VerticalTileCountComboBoxItems
        {
            get { return _verticalTileCountComboBoxItems; }
            set
            {
                if (Equals(value, _verticalTileCountComboBoxItems)) return;
                _verticalTileCountComboBoxItems = value;
                OnPropertyChanged();
            }
        }

        public TileCountComboBoxItem SelectedVerticalTileCountComboBoxItem
        {
            get { return _selectedVerticalTileCountComboBoxItem; }
            set
            {
                if (Equals(value, _selectedVerticalTileCountComboBoxItem)) return;
                _selectedVerticalTileCountComboBoxItem = value;
                OnPropertyChanged();
            }
        }

        public string HeightmapStartingElevation
        {
            get { return _heightmapStartingElevation; }
            set
            {
                if (value == _heightmapStartingElevation) return;
                _heightmapStartingElevation = value;
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

        public bool GenerateButtonEnabled
        {
            get { return _generateButtonEnabled; }
            set
            {
                if (value == _generateButtonEnabled) return;
                _generateButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand GenerateCommand => new RelayCommand(Generate);
        public ICommand BrowseCommand => new RelayCommand(Browse);
        public ICommand SelectionChangedCommand => new RelayCommand(SelectionChanged);
        public ICommand LoadedEventCommand => new RelayCommand(LoadedEvent);

        private void LoadedEvent(object obj)
        {
           
        }

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

            SelectedResolutionComboBoxItem = new ResolutionComboBoxItem { Id = 505 };

            HorizontalTileCountComboBoxItems = new List<TileCountComboBoxItem>();
            for (int i = 2; i < 31; i++)
            {
                HorizontalTileCountComboBoxItems.Add(new TileCountComboBoxItem() { Id = i, Value = i.ToString() });
            }
            SelectedHorizontalTileCountComboBoxItem = new TileCountComboBoxItem();

            VerticalTileCountComboBoxItems = new List<TileCountComboBoxItem>();
            for (int i = 2; i < 31; i++)
            {
                VerticalTileCountComboBoxItems.Add(new TileCountComboBoxItem() { Id = i, Value = i.ToString() });
            }
            SelectedVerticalTileCountComboBoxItem = new TileCountComboBoxItem();

            SelectedGenerationHeightComboBoxItem = new GenerationHeightComboBoxItem();
            GenerationHeightComboBoxItems = new ObservableCollection<GenerationHeightComboBoxItem>();
            for (int i = 1; i < 160; i++)
            {
                var level = i * 100;
                GenerationHeightComboBoxItems.Add(
                    new GenerationHeightComboBoxItem {Id = level, Level = level.ToString()});

            }

            SelectedImageOutputType = new ComboBoxItem();
            ImageOutputTypes = new ObservableCollection<ComboBoxItem>();
            ImageOutputTypes.Add(new ComboBoxItem() { Id = 0, Value = ".raw" });
            ImageOutputTypes.Add(new ComboBoxItem() { Id = 0, Value = ".png" });
            ImageOutputTypes.Add(new ComboBoxItem() { Id = 0, Value = ".bmp" });

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

                var heightmapStartingElevation = 0;
                if (!int.TryParse(SelectedGenerationHeightComboBoxItem.Level, out heightmapStartingElevation))
                {
                    MessageBox.Show("Starting elevation needs to be a number!", "Error", MessageBoxButton.OK);
                    return;
                }

                if (heightmapStartingElevation > 65000)
                {
                    MessageBox.Show("Starting elevation must be less than 65000!", "Error", MessageBoxButton.OK);
                    return;
                }

                long dataQuantity = ((long)SelectedResolutionComboBoxItem.Id * (long)SelectedResolutionComboBoxItem.Id *
                                     (long)SelectedVerticalTileCountComboBoxItem.Id * (long)SelectedHorizontalTileCountComboBoxItem.Id) * (long)2; //In bytes

                var driveSpaceLeft = GetDriveSpace();
                if (dataQuantity > driveSpaceLeft)
                {
                    MessageBox.Show(
                        $"This will generate {FormatBytes(dataQuantity)} which is more than your current available drive space: {FormatBytes(driveSpaceLeft)}.  Canceling..",
                        "Too much disk space required.", MessageBoxButton.OK);

                    return;
                }


                if (MessageBox.Show($"This will generate {FormatBytes(dataQuantity)} of heightmaps, are you sure you want to continue?",
                        "Proceed?", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return;

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

                //Open browse file dialog
                Browse(null);

                var elevationBytes = BitConverter.GetBytes(heightmapStartingElevation);

                for (int yTileIndex = 0; yTileIndex < SelectedVerticalTileCountComboBoxItem.Id; yTileIndex++)
                {
                    for (int xTileIndex = 0; xTileIndex < SelectedHorizontalTileCountComboBoxItem.Id; xTileIndex++)
                    {
                        var filename = $"{BaseDirectory}\\{HeightmapFilenames}_X{xTileIndex}_Y{yTileIndex}.raw";

                        var writeStream = new FileStream(filename, FileMode.OpenOrCreate);
                        BinaryWriter writeBinay = new BinaryWriter(writeStream);
                        for (int x = 0; x < SelectedResolutionComboBoxItem.Id; x++)
                        {
                            for (int y = 0; y < SelectedResolutionComboBoxItem.Id; y++)
                            {
                                writeBinay.Write(elevationBytes);
                            }
                        }

                        writeBinay.Close();
                    }
                }

                MessageBox.Show("Heightmaps generated.", "Success", MessageBoxButton.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void Generate2(object obj)
        {
            try
            {
                if (HeightmapFilenamesValid())
                {
                    var heightmapStartingElevation = 0;
                    if (StartingElevationIsNumeric(out heightmapStartingElevation))
                    {
                        if (StartingElevationValid(heightmapStartingElevation))
                        {
                            long dataQuantity = ((long)SelectedResolutionComboBoxItem.Id * (long)SelectedResolutionComboBoxItem.Id *
                                                 (long)SelectedVerticalTileCountComboBoxItem.Id * (long)SelectedHorizontalTileCountComboBoxItem.Id) * (long)2; //In bytes

                            var driveSpaceLeft = GetDriveSpace();
                            if (EnoughDriveSpaceAvailable(dataQuantity, driveSpaceLeft))
                            {
                                if (MessageBox.Show(
                                        $"This will generate {FormatBytes(dataQuantity)} of heightmaps, are you sure you want to continue?",
                                        "Proceed?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                                {
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

                                    var elevationBytes = BitConverter.GetBytes(heightmapStartingElevation);

                                    for (int yTileIndex = 0; yTileIndex < SelectedVerticalTileCountComboBoxItem.Id; yTileIndex++)
                                    {
                                        for (int xTileIndex = 0; xTileIndex < SelectedHorizontalTileCountComboBoxItem.Id; xTileIndex++)
                                        {
                                            var filename = $"{BaseDirectory}\\{HeightmapFilenames}_X{xTileIndex}_Y{yTileIndex}.raw";

                                            var writeStream = new FileStream(filename, FileMode.OpenOrCreate);
                                            BinaryWriter writeBinay = new BinaryWriter(writeStream);
                                            for (int x = 0; x < SelectedResolutionComboBoxItem.Id; x++)
                                            {
                                                for (int y = 0; y < SelectedResolutionComboBoxItem.Id; y++)
                                                {
                                                    writeBinay.Write(elevationBytes);
                                                }
                                            }

                                            writeBinay.Close();
                                        }
                                    }

                                    MessageBox.Show("Heightmaps generated.", "Success", MessageBoxButton.OK);
                                }
                            }
                            else
                            {
                                MessageBox.Show(
                                    $"This will generate {FormatBytes(dataQuantity)} which is more than your current available drive space: {FormatBytes(driveSpaceLeft)}.  Canceling..",
                                    "Too much disk space required.", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Starting elevation must be less than 65000!", "Error", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Starting elevation needs to be a number!", "Error", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Heightmap filenames required!", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        private bool StartingElevationIsNumeric(out int heightmapStartingElevation)
        {
            return int.TryParse(HeightmapStartingElevation, out heightmapStartingElevation);
        }

        private static bool EnoughDriveSpaceAvailable(long dataQuantity, long driveSpaceLeft)
        {
            return dataQuantity < driveSpaceLeft;
        }

        private bool HeightmapFilenamesValid()
        {
            return !string.IsNullOrEmpty(HeightmapFilenames);
        }

        private static bool StartingElevationValid(int heightmapStartingElevation)
        {
            return heightmapStartingElevation < 65000;
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

        private string FormatBytes(double bytes)
        {
            double doubleBytes;
            if (bytes >= 1099511627776L)
            {
                doubleBytes = (double)bytes / 1099511627776L;
                return Math.Round(doubleBytes, 2) + " TB";
            }
            if (bytes >= 1073741824 && bytes < 1099511627775)
            {
                doubleBytes = (double)bytes / 1073741824;
                return Math.Round(doubleBytes, 2) + " GB";
            }
            if (bytes >= 1048576 && bytes < 1073741823)
            {
                doubleBytes = (double)bytes / 1048576;
                return Math.Round(doubleBytes, 2) + " MB";
            }
            if (bytes >= 1024 && bytes < 1048575)
            {
                doubleBytes = (double)bytes / 1024;
                return Math.Round(doubleBytes, 2) + " KB";
            }
            if (bytes >= 0 && bytes < 1023)
            {
                doubleBytes = (double)bytes;
                return Math.Round(doubleBytes, 2) + " Bytes";
            }
            return null;
        }

        private long GetDriveSpace()
        {
            string driveName = Path.GetPathRoot(Environment.SystemDirectory);
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        private void SelectionChanged(object obj)
        {
            if (!string.IsNullOrEmpty(SelectedVerticalTileCountComboBoxItem.Value) &&
                !string.IsNullOrEmpty(SelectedHorizontalTileCountComboBoxItem.Value) &&
                !string.IsNullOrEmpty(SelectedResolutionComboBoxItem.Resolution) &&
                !string.IsNullOrEmpty(SelectedGenerationHeightComboBoxItem.Level))
                GenerateButtonEnabled = true;
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
