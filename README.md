# JsonDiffer.NetStandard
A lightweight utility to compare JSON objects and hence practically any serialize-able entity


## JSON 1
```javascript
{
  "name":"John",
  "age":30,
  "cars": {
    "car1":"Ford",
    "car2":"BMW",
    "car3":"Fiat"
  }
 }
```

## JSON 2
```javascript
{
  "name":"John",
  "cars": {
    "car1":"Ford",
    "car2":"BMW",
    "car3":"Audi",
    "car4":"Jaguar"
  }
 }
```

## Usage 
```csharp

 var j1 = JToken.Parse(Read(json1));
 var j2 = JToken.Parse(Read(json2));
 
 var diff = JsonDifferentiator.Differentiate(j1,j2);
 
```

## Result 
```javascript
{
  "-age": 30,
  "*cars": {
    "*car3": "Fiat",
    "+car4": "Jaguar"
  }
}
```
