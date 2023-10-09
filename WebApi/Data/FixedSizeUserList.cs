using LocalDBWebApiUsingEF.Models;

namespace WebApi.Data {
    public class FixedSizeUserList {
        private static FixedSizeUserList _instance;
        private static object _lockObject = new object();

        private List<User> _users;
        private int _size;
        private UserGenerator _userGenerator;

        private const int NUMBER_OF_ENTRIES = 1_000;

        private FixedSizeUserList(int size) {
            _size = size;
            _users = new List<User>(size);
            _userGenerator = new UserGenerator();
            PopulateUsers();
        }

        public static FixedSizeUserList GetInstance() {
            if (_instance == null) {
                lock (_lockObject) {
                    if (_instance == null) {
                        _instance = new FixedSizeUserList(NUMBER_OF_ENTRIES);
                    }
                }
            }
            return _instance;
        }

        private void PopulateUsers() {
            for (int i = 0; i < _size; i++) {
                _userGenerator.GetNextAccount(
                    out string username,
                    out string name,
                    out string email,
                    out string address,
                    out string phone,
                    out byte[] picture,
                    out string password);

                User user = new User(username) {
                    Name = name,
                    Email = email,
                    Address = address,
                    Phone = phone,
                    Picture = picture,
                    Password = password
                };

                _users.Add(user);
            }
        }

        public List<User> GetUsers() {
            return _users;
        }

        public User GetUser(int index) {
            return _users[index];
        }
    }
}
