//Get lista dell'oggetto
List<System.Reflection.PropertyInfo> personaProperties = persona.GetType().GetProperties().ToList();
List<string> BlackList = new List<string>() {"privilegi", "filiale" };

//Itero tutte le properties della persona rimuovendo quelle in comune con account e le aggiungo a quest' ultimo
//!!!Non viene modificata la classe ma solo una lista che contiene i nomi delle propietà della classe

int? tempNullInt;
for (int index = 0; index < personaProperties.Count; index++) {
    if (!BlackList.Contains(personaProperties[index].Name)) {
        
        temp = Input.ReadString("Nuovo {0}", personaProperties[index].Name);

        if (!string.IsNullOrWhiteSpace(temp)){
            
            //int
            if (personaProperties[index].GetValue(persona).GetType() == typeof(int?)) {
                    int.TryParse(temp, out tempInt); 
                    
                    personaProperties[index].SetValue(persona, tempInt);

                    temp = string.Empty;
                }
            } else if (personaProperties[index].GetValue(persona).GetType() == typeof(DateTime?)) { //Date time
                if (((DateTime?)personaProperties[index].GetValue(persona)).HasValue) {

                }
            } else {
                if (personaProperties[index].GetValue(persona).ToString() != string.Empty) { //string 
                    
                }
            }
        }
    }
}
