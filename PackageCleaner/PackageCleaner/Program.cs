using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Management.Deployment;

namespace PackageCleaner {
    class Program {
        static void Main(string[] args) {
            // http://stackoverflow.com/questions/9208921/async-on-main-method-of-console-app
            Task.Run(async () => {
                await FindPackages();
            }).Wait();
        }

        private async static Task FindPackages() {
            PackageManager packageManager = new PackageManager();

            IEnumerable<Windows.ApplicationModel.Package> packages =
                (IEnumerable<Windows.ApplicationModel.Package>)packageManager.FindPackages();

            int packageCount = 0;
            foreach (var package in packages) {
                if (
                        (package.Id.Publisher.Contains("CN=Ron") || 
                        package.Id.Publisher.Contains("CN=Bob") || 
                        package.Id.FullName.StartsWith("Ron.")) 
                        && 
                        (!package.Id.FullName.StartsWith("Keep."))
                    )
                {
                    DisplayPackageInfo(package);
                    await packageManager.RemovePackageAsync(package.Id.FullName);
                    Console.WriteLine("=========================================================");
                }
                packageCount += 1;
            }

            if (packageCount < 1) {
                Console.WriteLine("No packages were found.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private static void DisplayPackageInfo(Windows.ApplicationModel.Package package) {
            Console.WriteLine("Name: {0}", package.Id.Name);

            Console.WriteLine("FullName: {0}", package.Id.FullName);

            Console.WriteLine("Version: {0}.{1}.{2}.{3}", package.Id.Version.Major, package.Id.Version.Minor,
                package.Id.Version.Build, package.Id.Version.Revision);

            Console.WriteLine("Publisher: {0}", package.Id.Publisher);

            Console.WriteLine("PublisherId: {0}", package.Id.PublisherId);

            try {
                Console.WriteLine("Installed Location: {0}", package.InstalledLocation.Path);
            }
            catch {

            }

            Console.WriteLine("IsFramework: {0}", package.IsFramework);
        }
    }
}
