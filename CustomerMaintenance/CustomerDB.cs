namespace CustomerMaintenance
{
    public static class CustomerDB
    {
        // TODO: Add the directory and path here
        private const string dir = @"..\..\..\..\C#\"; 
                                                       
                                                                   
        private const string path = dir + "customers.dat";
        private const string sep = "|";


        public static void SaveCustomers(List<Customer> customers)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            using FileStream saveCustomer = new FileStream(path, FileMode.Create, FileAccess.Write);
            using BinaryWriter textOut = new BinaryWriter(saveCustomer);
            foreach (Customer c in customers)
            {
                textOut.Write(c.FirstName);
                textOut.Write(c.LastName);
                textOut.Write(c.Email);
                    }
        }

        public static List<Customer> GetCustomers() 
        { 
            List<Customer> customers = new(); 
                                              
            if (!File.Exists(path))
                return customers;  

            using FileStream loadCustomer = new FileStream(path, FileMode.Open, FileAccess.Read);                                                                //textIn (variable name for this particular reader),
            using BinaryReader textIn = new BinaryReader(loadCustomer); //using directive, StreamReader (class that Reads from a text file),

                
            while (loadCustomer.Position < loadCustomer.Length)
            {
                string first = textIn.ReadString();
                string last = textIn.ReadString();
                string email = textIn.ReadString();

                Customer c = new Customer
                {

                    FirstName = first,
                    LastName = last,
                    Email = email
                };
                customers.Add(c);
            };  
            return customers;                  
        }   
    }
}