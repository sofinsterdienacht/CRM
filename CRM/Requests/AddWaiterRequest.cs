﻿namespace MyCRM.Requests;

public class AddWaiterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public int Phone { get; set; }
}