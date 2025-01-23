using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;
using Path = System.IO.Path;

namespace ZipVolokSoft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Drag & Drop для добавления файлов
        private void Window_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy; // Указываем, что копирование разрешено
            e.Handled = true; // Отмечаем событие как обработанное
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (File.Exists(file)) // Проверяем, что это файл
                    {
                        FileList.Items.Add(file);
                    }
                }

                // Скрываем подсказку, если файлы добавлены
                if (FileList.Items.Count > 0)
                {
                    DropHint.Visibility = Visibility.Collapsed;
                }
            }
            e.Handled = true; // Отмечаем событие как обработанное
        }

        private void CreateArchive_Click(object sender, RoutedEventArgs e)
        {
            if (FileList.Items.Count == 0)
            {
                MessageBox.Show("Добавьте файлы для архивации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создаем диалог для выбора имени архива и папки
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "ZIP Archive (*.zip)|*.zip|7Z Archive (*.7z)|*.7z",
                Title = "Сохранить архив"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string archivePath = saveFileDialog.FileName;

                try
                {
                    // Определяем формат архива
                    if (archivePath.EndsWith(".zip"))
                    {
                        using (var archive = SharpCompress.Archives.Zip.ZipArchive.Create())
                        {
                            foreach (var item in FileList.Items)
                            {
                                string filePath = item.ToString();
                                archive.AddEntry(Path.GetFileName(filePath), File.OpenRead(filePath));
                            }
                            archive.SaveTo(archivePath, CompressionType.Deflate);
                        }
                    }
                    else if (archivePath.EndsWith(".7z"))
                    {
                        CreateSevenZipArchive(archivePath);
                    }

                    MessageBox.Show($"Архив успешно создан: {archivePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании архива: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        // Метод для создания 7z-архива с использованием библиотеки SevenZipSharp
        private SevenZip.CompressionLevel selectedCompressionLevel = SevenZip.CompressionLevel.Ultra;

        private void CreateSevenZipArchive(string archivePath)
        {
            try
            {
                SevenZip.SevenZipCompressor compressor = new SevenZip.SevenZipCompressor
                {
                    CompressionLevel = selectedCompressionLevel, // Используем выбранный уровень сжатия
                    CompressionMethod = SevenZip.CompressionMethod.Lzma2 // Метод сжатия
                };

                // Список файлов для добавления в архив
                List<string> files = new List<string>();
                foreach (var item in FileList.Items)
                {
                    files.Add(item.ToString());
                }

                compressor.CompressFiles(archivePath, files.ToArray());

                MessageBox.Show($"Архив 7z успешно создан: {archivePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании архива 7z: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Обработчик для распаковки архива
        private void ExtractArchive_Click(object sender, RoutedEventArgs e)
        {
            // Открытие диалога для выбора архива
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Архивы (*.zip;*.7z)|*.zip;*.7z",
                Title = "Выберите архив для распаковки"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string archivePath = openFileDialog.FileName;

                // Создаем диалог для выбора папки для распаковки
                var folderDialog = new System.Windows.Forms.FolderBrowserDialog
                {
                    Description = "Выберите папку для распаковки",
                    ShowNewFolderButton = true
                };

                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string extractPath = folderDialog.SelectedPath;

                    try
                    {
                        // Открытие архива и распаковка файлов
                        using (var archive = ArchiveFactory.Open(archivePath))
                        {
                            foreach (var entry in archive.Entries)
                            {
                                if (!entry.IsDirectory)
                                {
                                    entry.WriteToDirectory(extractPath, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
                                }
                            }

                            MessageBox.Show($"Архив успешно распакован в папку: {extractPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при распаковке архива: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        // Обработчик для просмотра содержимого архива
        private void ViewArchive_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Архивы (*.zip;*.7z)|*.zip;*.7z",
                Title = "Выберите архив для просмотра"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string archivePath = openFileDialog.FileName;

                try
                {
                    using (var archive = ArchiveFactory.Open(archivePath))
                    {
                        FileList.Items.Clear(); // Очищаем список файлов

                        foreach (var entry in archive.Entries)
                        {
                            if (!entry.IsDirectory)
                            {
                                FileList.Items.Add($"{entry.Key} ({entry.Size} байт)");
                            }
                        }

                        DropHint.Visibility = Visibility.Collapsed; // Скрыть подсказку
                        MessageBox.Show("Содержимое архива успешно загружено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при просмотре архива: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую кликнули
            var button = sender as Button;
            if (button != null)
            {
                // Получаем элемент, которому принадлежит эта кнопка (это будет элемент в ListBox)
                var listItem = button.DataContext as string;
                if (listItem != null)
                {
                    // Удаляем элемент из списка
                    FileList.Items.Remove(listItem);
                }
            }
        }

        private void CompressionLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный элемент
            var selectedItem = CompressionLevel.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                // Получаем текст выбранного элемента
                string selectedCompression = selectedItem.Content.ToString();

                // В зависимости от выбранного значения, изменяем уровень сжатия
                switch (selectedCompression)
                {
                    case "Низкий":
                        selectedCompressionLevel = SevenZip.CompressionLevel.High;
                        break;
                    case "Средний":
                        selectedCompressionLevel = SevenZip.CompressionLevel.Normal;
                        break;
                    case "Высокий":
                        selectedCompressionLevel = SevenZip.CompressionLevel.Ultra;
                        break;
                    default:
                        selectedCompressionLevel = SevenZip.CompressionLevel.Normal;
                        break;
                }
            }
        }

    }
}