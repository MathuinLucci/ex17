namespace CustomerMaintenance
{
    public static class CustomerDB
    {
        // TODO: Add the directory and path here
        private const string dir = @"..\..\..\..\C#\"; //"private": Only this *class* can see this variable. "For this class only"
                                                       //"const": This variable's value cannot be changed after it's initially set. "It's a constant."
                                                       // "string": The variable is a string of text. "It's text data."
        private const string path = dir + "customers.txt";
        private const string sep = "|";                // "sep" will always be a string value "|" and cannot be changed later through coding or variables

        public static void SaveCustomers(List<Customer> customers) //"Public": Any file, method, or namespace can see and use this. "Shared with anyone."
                                                                   //"Static": The method belongs to the class itself, not to any specific instance of the class. "It's here as a file of sorts, not createed and discarded like an instanced object"
                                                                   //"Void": This method does not return any value. "Just does its job and leaves. No return data."
                                                                   //In short: "This is a method anyone can call, without creating an object, and it doesn’t return anything."
        {
            if (!Directory.Exists(dir))         //if the directory does not(!) exist, 
                Directory.CreateDirectory(dir); //<-- execute the code here to create the directory


            using StreamWriter textOut = new StreamWriter(path); //"using" means the system will automatically close the StreamWriter when done. This is importnt because it usses system resources that need to be freed up when the operation is complete.
                                                                 //failure to use a "using directive" can quietly hold system resources, like RAM, hostage. This is also called a "memory leak".
                                                                 //"StreamWriter": This is a tool for writing text to a file. "It's a text file writer."
                                                                 //"textOut": This is the name of the StreamWriter variable. "It's what we call our text file writer."
                                                                 //"new StreamWriter(path)": This creates a new StreamWriter that will write to the file specified by the "path" variable. "It's set to write to our customers.txt file."

            foreach (Customer c in customers) //for each 
            {
                textOut.WriteLine(
                    c.FirstName + sep +
                    c.LastName + sep +
                    c.Email
                );
            }
        }

        public static List<Customer> GetCustomers() 
        {
            List<Customer> customers = new();

            if (!File.Exists(path))
                return customers;

            using StreamReader textIn = new StreamReader(path);

            while (textIn.Peek() != -1)
            {
                string row = textIn.ReadLine() ?? "";
                string[] columns = row.Split(sep);

                Customer c = new Customer
                {
                    FirstName = columns[0],
                    LastName = columns[1],
                    Email = columns[2]
                };

                customers.Add(c);
            }

            return customers;    
        }   
    }
}
