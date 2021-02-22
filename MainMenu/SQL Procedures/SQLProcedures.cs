using MainMenu.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static MainMenu.Models.TravelWarrant;

namespace MainMenu.SQL_Procedures
{
    public class SQLProcedures
    {
        private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private const string IDDRIVER = "@IDVozac";
        private const string NAME = "@Ime";
        private const string SURNAME = "@Prezime";
        private const string CELLNUMBER = "@Broj_Mobitela";
        private const string LICENSENUMBER = "@Broj_Vozacke_Dozvole";
        private const string IDPUTNINALOG = "@IDPutniNalog";
        private const string MJESTOPOLASKA = "@Mjesto_Polaska";
        private const string MJESTOPUTOVANJA = "@Mjesto_Putovanja";
        private const string VOZACID = "@VozacID";
        private const string VOZILOID = "@VoziloID";
        private const string DATUMIZDAVANJA = "@Datum_Izdavanja";
        private const string DATUMPREDAJE = "@Datum_Predaje";
        private const string IDVEHICLE = "@IDVozilo";
        private const string MARKA = "@Marka";
        private const string GODINA_PROIZVODNJE = "@Godina_Proizvodnje";
        private const string INICIJALNO_STANJE_KILOMETARA = "@Inicijalno_Stanje_Kilometara";
        private const string TIP_Vozila = "@Tip";
        private static string TIP;
        private const string IDSERVIS = "@IDServis";
        private const string NAZIV_SERVISA = "@Naziv_Servisa";
        private const string IDRACUN = "@IDRacun";
        private const string SERVISID = "@ServisID";

        internal static IEnumerable<Service> selectSveServise()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(selectSveServise);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Service
                            (
                               (int)dr[nameof(Service.IDServis)],
                               dr[nameof(Service.Naziv_Servisa)].ToString()                           
                            );
                        }
                    }
                }
            }
        }
        internal static IEnumerable<TravelWarrant> selectSvePutneNaloge()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(selectSvePutneNaloge);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (!dr[nameof(TravelWarrant.Mjesto_Polaska)].ToString().Equals("") && dr[nameof(TravelWarrant.Mjesto_Putovanja)].ToString().Equals(""))
                            {
                                TIP = Tip.Aktivan.ToString();
                            }
                            else if (dr[nameof(TravelWarrant.Mjesto_Polaska)].ToString().Equals("") && dr[nameof(TravelWarrant.Mjesto_Putovanja)].ToString().Equals(""))
                            {
                                TIP = Tip.Buduci.ToString();
                            }
                            else
                            {
                                TIP = Tip.Zatvoren.ToString();
                            }
                            yield return new TravelWarrant
                            (
                               (int)dr[nameof(TravelWarrant.IDPutniNalog)],
                               dr[nameof(TravelWarrant.Mjesto_Polaska)].ToString(),
                               dr[nameof(TravelWarrant.Mjesto_Putovanja)].ToString(),
                               (int)dr[nameof(TravelWarrant.VozacID)],
                               (int)dr[nameof(TravelWarrant.VoziloID)],
                               (DateTime)dr[nameof(TravelWarrant.Datum_Izdavanja)],
                               (DateTime)dr[nameof(TravelWarrant.Datum_Predaje)],
                               TIP.ToString()
                              
                        

                            );
                        }
                    }
                }
            }
        }

        internal static void createRacun(int vehicleID, int serviceID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(createRacun);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(SERVISID, serviceID);
                    cmd.Parameters.AddWithValue(VOZILOID, vehicleID);
                    SqlParameter idRacun = new SqlParameter(IDRACUN, System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idRacun);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void updateServis(int idService, string name)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(updateServis);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(NAZIV_SERVISA, name);
                    cmd.Parameters.AddWithValue(IDSERVIS, idService);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void createServis(string name)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(createServis);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(NAZIV_SERVISA, name);
                    SqlParameter idService = new SqlParameter(IDSERVIS, System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idService);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void deleteServis(int idService)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(deleteServis);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDSERVIS, idService);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void Con_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Console.WriteLine("An error has happened:\n" + e.Errors[0]);
        }

        internal static IEnumerable<Vehicles> selectSvaVozila()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(selectSvaVozila);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Vehicles
                            (
                               (int)dr[nameof(Vehicles.IDVozilo)],
                               dr[nameof(Vehicles.Tip)].ToString(),
                               dr[nameof(Vehicles.Marka)].ToString(),
                               (int)dr[nameof(Vehicles.Godina_Proizvodnje)],
                               (int)dr[nameof(Vehicles.Inicijalno_Stanje_Kilometara)]
                            );
                        }
                    }
                }
            }
        }

        internal static void createPutniNalog(string startingPlace, string endingPlace, int driverID, int vehicleID, string startingDate, string endingDate)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(createPutniNalog);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(MJESTOPOLASKA, startingPlace);
                    cmd.Parameters.AddWithValue(MJESTOPUTOVANJA, endingPlace);
                    cmd.Parameters.AddWithValue(VOZACID, driverID);
                    cmd.Parameters.AddWithValue(VOZILOID, vehicleID);
                    cmd.Parameters.AddWithValue(DATUMIZDAVANJA, startingDate);
                    cmd.Parameters.AddWithValue(DATUMPREDAJE, endingDate);
                    SqlParameter idWarrant = new SqlParameter(IDPUTNINALOG, System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idWarrant);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void updatePutniNalog(int idWarrant, string startingPlace, string endingPlace, int driverID, int vehicleID, DateTime startingDate, DateTime endingDate)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(updatePutniNalog);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(MJESTOPOLASKA, startingPlace);
                    cmd.Parameters.AddWithValue(MJESTOPUTOVANJA, endingPlace);
                    cmd.Parameters.AddWithValue(VOZACID, driverID);
                    cmd.Parameters.AddWithValue(VOZILOID, vehicleID);
                    cmd.Parameters.AddWithValue(DATUMIZDAVANJA, startingDate);
                    cmd.Parameters.AddWithValue(DATUMPREDAJE, endingDate);
                    cmd.Parameters.AddWithValue(IDPUTNINALOG, idWarrant);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void deletePutniNalog(int idWarrant)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(deletePutniNalog);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDPUTNINALOG, idWarrant);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static IEnumerable<Drivers> selectSveVozace()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(selectSveVozace);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Drivers
                            (
                                (int)dr[nameof(Drivers.IDVozac)],
                                dr[nameof(Drivers.Ime)].ToString(),
                                dr[nameof(Drivers.Prezime)].ToString(),
                                dr[nameof(Drivers.Broj_Mobitela)].ToString(),
                                dr[nameof(Drivers.Broj_Vozacke_Dozvole)].ToString()
                            );
                        }
                    }
                }
            }
        }
        
        internal static void resetDatabase()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(resetDatabase);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void createVozac(string name, string surname, string cellphone, string licenseNumber)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(createVozac);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(NAME, name);
                    cmd.Parameters.AddWithValue(SURNAME, surname);
                    cmd.Parameters.AddWithValue(CELLNUMBER, cellphone);
                    cmd.Parameters.AddWithValue(LICENSENUMBER, licenseNumber);
                    SqlParameter idDriver = new SqlParameter(IDDRIVER, System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idDriver);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void createVozilo(string tip, string marka, int godina_Proizvodnje, int inicijalno_Stanje_Kilometara)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(createVozilo);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(TIP_Vozila, tip);
                    cmd.Parameters.AddWithValue(MARKA, marka);
                    cmd.Parameters.AddWithValue(GODINA_PROIZVODNJE, godina_Proizvodnje);
                    cmd.Parameters.AddWithValue(INICIJALNO_STANJE_KILOMETARA, inicijalno_Stanje_Kilometara);
                    SqlParameter idVehicle = new SqlParameter(IDVEHICLE, System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idVehicle);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void updateVozac(int idDriver, string name, string surname, string cellphone, string licenseNumber)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(updateVozac);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(NAME, name);
                    cmd.Parameters.AddWithValue(SURNAME, surname);
                    cmd.Parameters.AddWithValue(CELLNUMBER, cellphone);
                    cmd.Parameters.AddWithValue(LICENSENUMBER, licenseNumber);
                    cmd.Parameters.AddWithValue(IDDRIVER, idDriver);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void deleteVozac(int idDriver)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.InfoMessage += Con_InfoMessage;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = nameof(deleteVozac);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IDDRIVER, idDriver);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}