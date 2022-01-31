# JsonDiffer.NetStandard
A lightweight utility to compare JSON objects and hence practically any serialize-able entity. This utility comes with two distincrt object models, adhoc and detailed.

* Adhoc object model shows diffrences with "*" for changed properties "-" and "+" for removed and added ones respectively.
* Detailed object models groups changes in three properties: changed, added and removed.

This library uses Newtonsoft.Json as the underlying engine.

** Legacy version should work as they did before thanks to optional parameters added to the new functionalities. **


# ADHOC Object Model
Shows diffrences with "*" for changed properties "-" and "+" for removed and added ones respectively.

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

## Usage 1
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

## Usage 2
```csharp

 var j1 = JToken.Parse(Read(json1));
 var j2 = JToken.Parse(Read(json2));
 
 var diff = JsonDifferentiator.Differentiate(j1,j2, showOriginalValues: true);
 
```


## Result 
```javascript
{
  "-age": 30,
  "*cars": {
    "*car3": "Audi",
    "+car4": "Jaguar"
  }
}


```
# Detailed Object Model
As mentioned this new object model groups changes in removed, changed and added properties in the result object recursively.


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

## Simple Usage 

### Example 1
```csharp

 var j1 = JToken.Parse(Read(json1));
 var j2 = JToken.Parse(Read(json2));
 
 var diff = JsonDifferentiator.Differentiate(j1,j2, outputMode = OutputMode.Detailed);
 
```

### Result 
```javascript
{
  "removed": {
    "age": 30
  },
  "changed": {
    "cars": {
      "changed": {
        "car3": "Fiat"
      },
      "added": {
        "car4": "Jaguar"
      }
    }
  }
}
```

### Example 2
```csharp

 var j1 = JToken.Parse(Read(json1));
 var j2 = JToken.Parse(Read(json2));
 
 var diff = JsonDifferentiator.Differentiate(j1,j2, outputMode = OutputMode.Detailed, showOriginalValues: true);
 
```

### Result 
```javascript
{
  "removed": {
    "age": 30
  },
  "changed": {
    "cars": {
      "changed": {
        "car3": "Audi"
      },
      "added": {
        "car4": "Jaguar"
      }
    }
  }
}
```

### Using Constructor for configuration
```csharp

 var j1 = JToken.Parse(Read(json1));
 var j2 = JToken.Parse(Read(json2));
 
 var differentiator = new JsonDifferentiator(OutputMode outputMode: OutputMode.Symbol, showOriginalValues: false);
 var diff = differentiator.Differentiate(j1,j2);
 
```

### Result 
```javascript
{
  "removed": {
    "age": 30
  },
  "changed": {
    "cars": {
      "changed": {
        "car3": "Fiat"
      },
      "added": {
        "car4": "Jaguar"
      }
    }
  }
}
```
