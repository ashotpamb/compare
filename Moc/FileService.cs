using System.Collections.Concurrent;
using Compare.Data;
using Compare.Models;

namespace Compare.Moc
{
    public class FileService
    {
        private readonly IServiceProvider _serviceProvider;

        public FileService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        static void WriteUserRecord(User user, StreamWriter streamWriter)
        {
            lock (streamWriter)
            {
                string record = $"{user.FirstName},{user.LastName},{user.Email},{user.PhoneNumber},{user.Password},{user.Address}";
                streamWriter.WriteLine(record);
            }

        }
        public void WriteToFile()
        {
            string filePath = "users.txt";
            using (var fileStreama = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var streamWriter = new StreamWriter(fileStreama))
                {
                    var users = new ConcurrentQueue<User>();
                    for (int i = 0; i <= 100000; i++)
                    {
                        User user = GenerateRanodmUser();
                        users.Enqueue(user);
                    }

                    Parallel.ForEach(users, user =>
                    {
                        WriteUserRecord(user, streamWriter);
                    });
                }
            }

        }
        private User GenerateRanodmUser()
        {
            var random = new Random();

            var user = new User
            {
                Id = random.Next(2000),
                FirstName = "testName",
                LastName = "TestLastName",
                Email = $"user{random.Next(1, 3000)}@example.com",
                Password = "somePassword",
                PhoneNumber = "9999999999",
                Address = "testAddres"

            };
            return user;
        }
        private static User ParseUser(string line)
        {
            string[] parts = line.Split(',');
            var user = new User
            {
                FirstName = parts[0].ToString(),
                LastName = parts[1].ToString(),
                Email = parts[2].ToString(),
                PhoneNumber = parts[3].ToString(),
                Password = parts[4].ToString(),
                Address = parts[5].ToString(),
            };

            return user;
        }

        public async Task ComapreAndAdd()
        {
            var filePath = "users.txt";
            var userObjects = System.IO.File.ReadAllLines(filePath)
            .Skip(1)
            .Select(ParseUser)
            .GroupBy(u => u.Email)
            .Select(grp => grp.First())
            .ToList();
            await CreateUserFromFile(userObjects);
        }

        public async Task CreateUserFromFile(List<User> userObjects)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetService<IUserRepository>();
                var pageSize = 100;
                var pageCount = (int)Math.Ceiling((double)userObjects.Count / pageSize);

                for (int i = 0; i < pageCount; i++)
                {
                    var batchRecords = userObjects.Skip(i * pageSize).Take(pageSize);

                    var databaseRecords = await userRepo.GetUsersInBatchAsync(batchRecords.Select(u => u.Email).ToList());

                    var differentRecords = batchRecords.Except(databaseRecords).ToList();

                    if (differentRecords.Any())
                    {
                        await userRepo.SaveUsersAsync(differentRecords);
                    }
                }


            };

        }
    }
}