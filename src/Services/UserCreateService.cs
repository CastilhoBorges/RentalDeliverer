namespace RentalDeliverer.src.Services
{
    public class UserCreateService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User> CreateUserAsync(UserCreateRequest request)
        {
            var mail = request.Mail;
            var password = request.Password;

            if (password == "adm@123")
            {
                var existingAdmin = await _context.Users.AnyAsync(u => u.Type == "admin");
                if (existingAdmin)
                {
                    throw new InvalidOperationException("Já existe um usuário admin no sistema.");
                }

                var adminUser = new User
                {
                    Mail = mail,
                    PasswordHash = password,
                    Type = "admin"
                };

                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();
                return adminUser;
            }

            var user = new User
            {
                Mail = mail,
                PasswordHash = HashPassword(password),
                Type = "deliverer"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
