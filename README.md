# JsonDiffer.NetStandard
A lightweight utility to compare JSON objects and hence practically any serialize-able entity


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


```javascript
{
  "name":"John",
  "cars": {
    "car1":"Ford",
    "car2":"BMW",
    "car3":"Audi"
    "car4":"Jaguar"
  }
 }
```
