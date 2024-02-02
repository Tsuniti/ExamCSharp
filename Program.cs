using ExamCSharp;

var dbManagment = new DBManagment();

dbManagment.AddUser("Admin", "admin");
dbManagment.AddCharacterByLogin("Admin", "God", 999);
dbManagment.PrintAllCharactersInfo();