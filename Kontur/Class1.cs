using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace Kontur
{
    internal class Class1
    {
        string test = "C:\\Users\\Admin\\Downloads\\Тестовые данные.csv";
        public void Remove()
        {
            using (var db = new KonturContext())
            {
                var datadel = db.Data.ToList();
                db.Data.RemoveRange(datadel);
                var codedel = db.Codes.ToList();
                db.Codes.RemoveRange(codedel);
                var catdel = db.Categories.ToList();
                db.Categories.RemoveRange(catdel);
                var depdel = db.Departments.ToList();
                db.Departments.RemoveRange(depdel);
                db.SaveChanges();
            }
            MessageBox.Show("Данные очищены");
        }
        public void Write()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.Delimiter = ";";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1251 = Encoding.GetEncoding(1251);
            using (var reader = new StreamReader(test, enc1251))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    var codelist = new List<Code>();
                    List<string> dep = new List<string>();
                    using (var db = new KonturContext())
                    {
                        while (csv.Read())
                        {
                            string category = csv.GetField<string>("Категория процесса");
                            string code = csv.GetField<string>("Код процесса");
                            string name = csv.GetField<string>("Наименование процесса");
                            string department = csv.GetField<string>("Подразделение-владелец процесса");
                            dep.Add(category);
                            codelist.Add(new Code
                            {
                                Code1 = code,
                                CodeName = name
                            });
                            var prov = db.Categories.SingleOrDefault(c => c.Name == category);
                            if (prov == null)
                            {
                                var data = new Category
                                {
                                    Name = category
                                };
                                db.Categories.Add(data);
                                db.SaveChanges();
                            }
                            var prov2 = db.Departments.SingleOrDefault(c => c.Name == department);
                            if (prov2 == null)
                            {
                                var data = new Department
                                {
                                    Name = department
                                };
                                db.Departments.Add(data);
                                db.SaveChanges();
                            }
                        }
                        int k = 0;
                        foreach(var cd in codelist)
                        {
                            var prov1 = db.Codes.SingleOrDefault(c => c.Code1 == cd.Code1);
                            if (prov1 == null)
                            {
                                int dd = db.Categories.Where(c => c.Name == dep[k]).Select(c => c.Id).Single();
                                cd.CatId = dd;
                                db.Codes.Add(cd);
                                db.SaveChanges();
                            }
                            k++;
                        }
                    }
                }
                MessageBox.Show("Вспомогательные данные добавлены");
            }
        }
        public void Read()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.Delimiter = ";";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1251 = Encoding.GetEncoding(1251);
            
            using (var db = new KonturContext())
            {
                using (var reader = new StreamReader(test, enc1251))
                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            string code = csv.GetField<string>("Код процесса");
                            string department = csv.GetField<string>("Подразделение-владелец процесса");
                            int k = db.Departments.Where(p => p.Name == department).Select(p => p.Id).Single();
                            var data = new Datum
                            {
                                CodeId = code,
                                DepId = k
                            };
                            db.Data.Add(data);
                            db.SaveChanges();
                        }
                    }
                    MessageBox.Show("Основные данные добавлены");
                }
            }

        }
    }
}
