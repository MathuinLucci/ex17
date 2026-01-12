namespace CustomerMaintenance
{
    public static class CustomerDB
    {
        // TODO: Add the directory and path here
        private const string dir = @"..\..\..\..\C#\"; //"private": Only this *class* can see this variable. "For this class only"
                                                       //"const": This variable's value cannot be changed after it's initially set.
                                                       //"It's a constant."
                                                       // "string": The variable is a string of text. "It's text data."
        private const string path = dir + "customers.txt";
        private const string sep = "|";                // "sep" will always be a string value "|" and cannot be changed later through
                                                       // coding or variables

        public static void SaveCustomers(List<Customer> customers) //"Public": Any file, method, or namespace can see and use this.
                                                                   //"Shared with anyone."
                                                                   //"Static": The method belongs to the class itself, not to any specific instance of
                                                                   //the class. "It's here as a file of sorts, not createed and discarded like
                                                                   //an instanced object"
                                                                   //"Void": This method does not return any value. "Just does its job and leaves.
                                                                   //No return data."
                                                                   //In short: "This is a method anyone can call, without creating an object,
                                                                   //and it doesn’t return anything."
        {
            if (!Directory.Exists(dir))         //if the directory does not(!) exist, 
                Directory.CreateDirectory(dir); //<-- execute the code here to create the directory


            using StreamWriter textOut = new StreamWriter(path); //"using" means the system will automatically close the StreamWriter when done. This is
                                                                 //importnt because it usses system resources that need to be freed up when the operation
                                                                 //is complete. Failure to use a "using directive" can quietly hold system resources
                                                                 //like RAM hostage. This is also called a "memory leak".
                                                                 //"StreamWriter": This is a tool for writing text to a file. "It's a text
                                                                 //file writer."
                                                                 //"textOut": This is the name of the StreamWriter variable.
                                                                 //"It's what we call our text file writer."
                                                                 //"new StreamWriter(path)": This creates a new StreamWriter that will write
                                                                 //to the file specified by the "path" variable. "It's set to write to our
                                                                 //customers.txt file."

            foreach (Customer c in customers) //for each customer "c" (c is the name of the variable, only used here) in the list of customers,
                                              //execute the following code:
            {
                textOut.WriteLine(      //textOut: our StreamWriter writing to the file.
                                        //WriteLine: This method writes a line of text to the file.
                    c.FirstName + sep + //c (customer).FirstName(the FirstName string assigned to each Customer object,
                                        //then adds sep(the pipe) at the end
                    c.LastName + sep +  //c (customer).LastName(the LastName string assigned to each Customer object),
                                        //then adds sep(the pipe) at the end
                    c.Email             //c (customer).Email(the Email string assigned to each Customer object). No sep here.
                );                      //and it will do this until it has looped through all customers in the list.
            }
        }

        public static List<Customer> GetCustomers() // public: accessible by anyone
                                                    //"Static": The method belongs to the class itself, not to any specific instance of
                                                    //the class. "It's here as a file of sorts,
                                                    //not createed and discarded like an instanced object"
        {                                           //List<Customer>: This method returns a list of Customer objects. "It gives back
                                                    //a list of customers."
                                                    //"GetCustomers()": The name of the method. "It's called GetCustomers."
            List<Customer> customers = new(); //a generic List<> that holds Customer objects named customers
                                              //"new()": This creates a new instance of the List<Customer> class. "It's a new list of customers."
                                              //This is instanced, therefore also not static like the method itself.
                                              //The method contains code to creade the instance of this list to iterate through,
                                              //using data from the customer list

            if (!File.Exists(path)) //If the file does not(!) exist at the specified path,
                return customers;   //<-- return the empty list of customers. (No file means no customers to load.)
                                    //"If the file does not exisst, return an empty list because there's nothing to read."
                                    //This prevents File Not Found errors if the file is missing.

            using StreamReader textIn = new StreamReader(path); //using directive, StreamReader (class that Reads from a text file),
                                                                //textIn (variable name for this particular reader),
                                                                //new StreamReader(path) (opens and reads the file at the specified path

            while (textIn.Peek() != -1)  //textIn.Peek() Looks at the next character in the file without moving forward
                                         //Peek() returns -1 when the end of the file is reached, so 
                                         //textIn.Peek() != -1 means to continue as long as the value of
                                         //Peek() is not equal to -1.
                                         //"Keep looking as long as we haven't reached the end of the file"
            {
                string row = textIn.ReadLine() ?? ""; // a string called row to hold one line of text from the file
                                                      // .ReadLine() reads the next full line from the file
                                                      //?? "" If the left side is null, use an empty string instead. Helps
                                                      //prevent null reference errors.
                                                      //"Read one line from the file. If null, use an empty string"

                string[] columns = row.Split(sep);    // a string array called columns to hold the parts of the row
                                                      //.Split(sep) splits the row into parts based on the separator (the pipe "|")
                                                      //"Split the line into parts using the pipe as the separator"

                Customer c = new Customer             // create a new Customer object named c
                                                      //"Create a new customer object"
                {
                    FirstName = columns[0],           // set the FirstName property to the first part of the columns array
                    LastName = columns[1],            // set the LastName property to the second part of the columns array
                    Email = columns[2]                // set the Email property to the third part of the columns array
                };

                customers.Add(c);                     // add the newly created Customer object to the customers list
                                                      //"Add this customer to the list of customers"
            }

            return customers;                         //"Return the list of customers that were read from the file"
        }   
    }
}