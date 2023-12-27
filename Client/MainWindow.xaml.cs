using Client.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //frame.Content = new PageMenu();
        }
        private void frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Замените URL на свой
                string apiUrl = "https://localhost:44355/api/Users";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<UserViewModel> users = JsonConvert.DeserializeObject<List<UserViewModel>>(json);
                    return users;
                }
                else
                {
                    // Обработка ошибок
                    return null;
                }
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            List<UserViewModel> users = await GetUsersAsync();

            if (users != null)
            {
                // Привязываем список пользователей к DataGrid
                userDataGrid.ItemsSource = users;
            }
        }
    }
}