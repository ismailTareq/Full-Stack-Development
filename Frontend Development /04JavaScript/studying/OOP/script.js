'use strict';

class Person {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
    greet() {
        console.log(`Hello, my name is ${this.name} and I am ${this.age} years old.`);
    }

    setAge(newAge) {
        if (newAge > 0) {
            this.age = newAge;
        } else {
            console.log('Please enter a valid age.');
        }
    }
    set name(newName) {
        if (newName.includes(' ')) {
        this._name = newName;
        } else {
            console.log('Please enter a full name.');
        }
    }
    get fullName() {
        return this._name;
    }
    get age() {
        return this._age;
    }

    static species() {
        return 'Homo sapiens';  
    }
}

// const x = new Person('John', 30);
// console.log(x.name);
// console.log(x.age);
// const y = new Person('Alice Smith', 25);
// console.log(y.name);
// console.log(y.age);
// let z = x.name;
// console.log(z);
// let a = Person.species();
console.log(Person.species());
// console.log(y.age);
// console.log(x.greet());
// y.setAge(-25);
// console.log(y.age);
// console.log(y.greet());

// console.log(x instanceof Person);
// console.log(y instanceof Person);
//     this.name = x
//     this.age = y
// }
// const x = new Func('John', 30)
// console.log(x.name)
// console.log(x.age)
// const y = new Func('Alice', 25)
// console.log(y.name)
// console.log(y.age)
// console.log(x instanceof Func)
// console.log(y instanceof Func)

// Func.prototype.greet = function() {
//     console.log(`Hello, my name is ${this.name} and I am ${this.age} years old.`)
// }
// console.log(Func.prototype)

// x.greet();
// y.greet();

// let arr = [1,2,6,4,5,6,7,6,9,10]
// console.log(arr.__proto__ === Array.prototype)
// console.log(Array.prototype)
// Array.prototype.unique = function() {
//     return [...new Set(this)]
// }
// console.log(arr.unique())

