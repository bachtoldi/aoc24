var  lines = File.ReadLines("input.txt");

var counter = 0;
var value = 50;
foreach(var line in lines)
{
    var direction = line[0];
    var amount = int.Parse(line[1..]);
    if(direction is 'L') amount *= -1;
    value = (value + amount) % 100;
    if(value is 0) counter++;
}

Console.WriteLine(counter);