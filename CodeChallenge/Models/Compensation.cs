using System;
using Microsoft.VisualBasic;

namespace CodeChallenge.Models;

public class Compensation
{
    public Employee Employee { get; set; }
    public int Salary { get; set; }
    public DateTime DateTime { get; set; }
}