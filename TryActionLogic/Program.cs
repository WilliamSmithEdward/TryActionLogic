namespace TryActionLogic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var errorList = new List<Exception>();

            List<string> myFileList = new List<string>()
            {
                @"C:\Users\willi\Documents\test1.txt",
                @"C:\Users\willi\Documents\test2.txt",
                @"C:\Users\willi\Documents\test3.txt",
                @"C:\Users\willi\Documents\test4.txt",
                @"C:\Users\willi\Documents\test5.txt"
            };

            var fileList = new List<FileStream>();

            myFileList.ForEach(x =>
            {
                var error = TryAction(() => fileList.Add(File.Open(x, FileMode.Open)));

                if (error != null) errorList.Add(error);
            });

            errorList.ForEach(x =>
            {
                Console.WriteLine(x.Message);
            });

            Console.WriteLine();

            fileList.ForEach(x =>
            {
                Console.WriteLine(x.Name);
            });

            if (errorList.Count == 0)
            {
                Console.WriteLine("Everything went great!");
            }

            else
            {
                Console.WriteLine("Oh crap! I need to take action!!");

                //Send an e-mail to let myself know
                //In the e-mail body, include the error stack trace, so I know which line of code the logic failed on
            }
        }

        public static Exception TryAction(Action action)
        {
            Exception exception = null;

            try
            {
                action();
            }

            catch (Exception ex)
            {
                exception = ex;
            }

            return exception;
        }
    }
}