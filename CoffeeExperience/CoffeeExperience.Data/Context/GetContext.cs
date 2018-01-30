namespace CoffeeExperience.Data.Context
{
    public class GetContext : IGetContext
    {
        private Contexto _context;
        public Contexto Get()
        {
            if (_context == null)
                _context = new Contexto();
            return _context;
        }
    }
}
