namespace prueba_harold_pedraza.Domain;

public class Document
{
    public int IdDocument { get; set; }
}

public class DocumentCreate
{
    public string Name { get; set; }
    public string Sigla { get; set; }
}

public class DocumentFormatt: Document
{
    public string Name { get; set; }
}
