'use strict';
// let arr = ['a', 'b', 'c', 'd', 'e'];
// console.log(arr.slice(2));//[c,d,e]
// console.log(arr.slice(2, 4));//c,d
// console.log(arr.slice(-2));//d,e
// console.log(arr.slice(1, -2));//e
// console.log(arr.slice());//a,b,c,d,e
// console.log([...arr]);//a,b,c,d,e\

// arr.splice(-1);
// console.log(arr);
// arr.splice(1, 2);
// console.log(arr);

// let x = ['f', 'g', 'h', 'i'];
// console.log(x.reverse());
// console.log(x);

// let letters = arr.concat(x);
// console.log(letters);
// console.log(arr);
// console.log([...arr, ...x]);

// console.log(typeof(letters.join('-')));

// let y = [23, 11, 64];
// console.log(y.at(0));
// console.log(y.at(-1));
// console.log(y.at(y.length - 1));

// console.log('jonas'.at(0));
// console.log('jonas'.at(-1));

// foreach
// console.log('----foreach----');
// const x = [200, 450, -400, 3000, -650, -130, 70, 1300];
// for (const movement of x) {
//   if (movement > 0) {
//     console.log(`You deposited ${movement}`);
//   } else {
//     console.log(`You withdrew ${Math.abs(movement)}`);
//   }
// }

// x.forEach(function (movement) {
//   if (movement > 0) {
//     console.log(`You deposited ${movement}`);
//   } else {
//     console.log(`You withdrew ${Math.abs(movement)}`);
//   }
// });

// // Map
// console.log('----foreach map----');
// const y = new Map([
//   ['USD', 'United States dollar'],
//   ['EUR', 'Euro'],
//   ['GBP', 'Pound sterling'],
// ]);

// y.forEach(function (value, key) {
//   console.log(`${key}: ${value}`);
//   // console.log(map );
// });

// // Set
// console.log('----foreach set----');
// const z = new Set(['USD', 'GBP', 'USD', 'EUR', 'EUR']);
// z.forEach(function (value, _, set) {
//   console.log(`${value}: ${value}`);
//   console.log(set );
// });

// const x = Object.groupBy(accounts, acc => {
//   const y = acc.movements.length >= 7  ? 'active' : 'inactive';
//   return y;
// });
// console.log(x);


// console.log(Number.isNaN(20));
// console.log(Number.isNaN(1));
// console.log(Number.isNaN('20'));
// console.log(Number.isNaN(+'20X'));
// console.log(Number.isNaN(23 / 1));

// console.log(Number.MAX_SAFE_INTEGER)
// console.log(2 ** 53 - 1);
// console.log(2 ** 53 + 1);
// console.log(2 ** 53 + 2);
// console.log(123549841231315131355131151616n);  








// const x = movements.sort((a,b) => a - b);
// console.log(x);
// const createUsernames = function(acc){
//   acc.forEach(function(acc){
//     acc.username = acc.owner.toLowerCase().split(' ').map(name => name[0]).join('');
//   });
// }

// const x = movements.filter(mov => mov > 0)
//                    .map(mov => mov * 1.1)
//                    .reduce((acc,mov) => acc + mov,0);
// console.log(x);



// const eurToUsd = 1.1;
// const movementsUSD = movements.map(function(mov){
//   return mov * eurToUsd;
// });

// console.log(movements);
// console.log(movementsUSD);

// const movementsUSDfor = [];
// for (const mov of movements)
//   movementsUSDfor.push(mov * eurToUsd);
// console.log(movementsUSDfor);

// const x = 'Steven Thomas Williams'
// createUsernames(accounts);
// console.log(accounts);

// const deposits = movements.filter(function(mov){
//   return mov > 0;
// });
// console.log(deposits)

// const withdrawals = movements.filter(mov => mov < 0);
// console.log(withdrawals);
console.log(23 === 23.0);

// Base 10 - 0 to 9. 1/10 = 0.1. 3/10 = 3.3333333
// Binary base 2 - 0 1s
console.log(0.1 + 0.2);
console.log(0.1 + 0.2 === 0.3);

// Conversion
console.log(Number('23'));
console.log(+'23');

// Parsing
console.log(Number.parseInt('30px', 10));
console.log(Number.parseInt('e23', 10));

console.log(Number.parseInt('  2.5rem  '));
console.log(Number.parseFloat('  2.5rem  '));

// console.log(parseFloat('  2.5rem  '));

// Check if value is NaN
console.log(Number.isNaN(20));
console.log(Number.isNaN('20'));
console.log(Number.isNaN(+'20X'));
console.log(Number.isNaN(23 / 0));

// Checking if value is number
console.log(Number.isFinite(20));
console.log(Number.isFinite('20'));
console.log(Number.isFinite(+'20X'));
console.log(Number.isFinite(23 / 0));

console.log(Number.isInteger(23));
console.log(Number.isInteger(23.0));
console.log(Number.isInteger(23 / 0));



///////////////////////////////////////
// Math and Rounding

console.log(Math.sqrt(25));
console.log(25 ** (1 / 2));
console.log(8 ** (1 / 3));

console.log(Math.max(5, 18, 23, 11, 2));
console.log(Math.max(5, 18, '23', 11, 2));
console.log(Math.max(5, 18, '23px', 11, 2));

console.log(Math.min(5, 18, 23, 11, 2));

console.log(Math.PI * Number.parseFloat('10px') ** 2);

console.log(Math.trunc(Math.random() * 6) + 1);

const randomInt = (min, max) =>
  Math.floor(Math.random() * (max - min + 1)) + min;

console.log(randomInt(10, 20));
console.log(randomInt(0, 3));

// Rounding integers
console.log(Math.round(23.3));
console.log(Math.round(23.9));

console.log(Math.ceil(23.3));
console.log(Math.ceil(23.9));

console.log(Math.floor(23.3));
console.log(Math.floor('23.9'));

console.log(Math.trunc(23.3));

console.log(Math.trunc(-23.3));
console.log(Math.floor(-23.3));

// Rounding decimals
console.log((2.7).toFixed(0));
console.log((2.7).toFixed(3));
console.log((2.345).toFixed(2));
console.log(+(2.345).toFixed(2));


///////////////////////////////////////
// The Remainder Operator
console.log(5 % 2);
console.log(5 / 2); // 5 = 2 * 2 + 1

console.log(8 % 3);
console.log(8 / 3); // 8 = 2 * 3 + 2

console.log(6 % 2);
console.log(6 / 2);

console.log(7 % 2);
console.log(7 / 2);

const isEven = n => n % 2 === 0;
console.log(isEven(8));
console.log(isEven(23));
console.log(isEven(514));

labelBalance.addEventListener('click', function () {
  [...document.querySelectorAll('.movements__row')].forEach(function (row, i) {
    // 0, 2, 4, 6
    if (i % 2 === 0) row.style.backgroundColor = 'orangered';
    // 0, 3, 6, 9
    if (i % 3 === 0) row.style.backgroundColor = 'blue';
  });
});


///////////////////////////////////////
// Numeric Separators

// 287,460,000,000
const diameter = 287_460_000_000;
console.log(diameter);

const price = 345_99;
console.log(price);

const transferFee1 = 15_00;
const transferFee2 = 1_500;

const PI = 3.1415;
console.log(PI);

console.log(Number('230_000'));
console.log(parseInt('230_000'));


///////////////////////////////////////
// Working with BigInt
console.log(2 ** 53 - 1);
console.log(Number.MAX_SAFE_INTEGER);
console.log(2 ** 53 + 1);
console.log(2 ** 53 + 2);
console.log(2 ** 53 + 3);
console.log(2 ** 53 + 4);

console.log(4838430248342043823408394839483204n);
console.log(BigInt(48384302));

// Operations
console.log(10000n + 10000n);
console.log(36286372637263726376237263726372632n * 10000000n);
// console.log(Math.sqrt(16n));

const huge = 20289830237283728378237n;
const num = 23;
console.log(huge * BigInt(num));

// Exceptions
console.log(20n > 15);
console.log(20n === 20);
console.log(typeof 20n);
console.log(20n == '20');

console.log(huge + ' is REALLY big!!!');

// Divisions
console.log(11n / 3n);
console.log(10 / 3);


///////////////////////////////////////
// Creating Dates

// Create a date

const now = new Date();
console.log(now);

console.log(new Date('Aug 02 2020 18:05:41'));
console.log(new Date('December 24, 2015'));
console.log(new Date(account1.movementsDates[0]));

console.log(new Date(2037, 10, 19, 15, 23, 5));
console.log(new Date(2037, 10, 31));

console.log(new Date(0));
console.log(new Date(3 * 24 * 60 * 60 * 1000));


// Working with dates
const future = new Date(2037, 10, 19, 15, 23);
console.log(future);
console.log(future.getFullYear());
console.log(future.getMonth());
console.log(future.getDate());
console.log(future.getDay());
console.log(future.getHours());
console.log(future.getMinutes());
console.log(future.getSeconds());
console.log(future.toISOString());
console.log(future.getTime());

console.log(new Date(2142256980000));

console.log(Date.now());

future.setFullYear(2040);
console.log(future);


///////////////////////////////////////
// Operations With Dates
const future = new Date(2037, 10, 19, 15, 23);
console.log(+future);

const calcDaysPassed = (date1, date2) =>
  Math.abs(date2 - date1) / (1000 * 60 * 60 * 24);

const days1 = calcDaysPassed(new Date(2037, 3, 4), new Date(2037, 3, 14));
console.log(days1);


///////////////////////////////////////
// Internationalizing Numbers (Intl)
const num = 3884764.23;

const options = {
  style: 'currency',
  unit: 'celsius',
  currency: 'EUR',
  // useGrouping: false,
};

console.log('US:      ', new Intl.NumberFormat('en-US', options).format(num));
console.log('Germany: ', new Intl.NumberFormat('de-DE', options).format(num));
console.log('Syria:   ', new Intl.NumberFormat('ar-SY', options).format(num));
console.log(
  navigator.language,
  new Intl.NumberFormat(navigator.language, options).format(num)
);


///////////////////////////////////////
// Timers

// setTimeout
const ingredients = ['olives', 'spinach'];
const pizzaTimer = setTimeout(
  (ing1, ing2) => console.log(`Here is your pizza with ${ing1} and ${ing2} 🍕`),
  3000,
  ...ingredients
);
console.log('Waiting...');

if (ingredients.includes('spinach')) clearTimeout(pizzaTimer);

// setInterval
setInterval(function () {
  const now = new Date();
  console.log(now);
}, 1000);
*/



// const balance = movements.reduce(function(acc,cur,i,arr){
//   return acc + cur;
// },0);
// console.log(balance);
// const x = function(y)
// {
//     return function(z){
//         console.log(`${y} ${z}`)
//     }
// }

// let a = x('Hey')
// a('ismail')
// x('ismail')('tarek')
// const x = function(str){
//     return str.replace(/ /g,'').toLowerCase();
// }

// const y = function(str){
//     const [first,...other] = str.split(' ')
//     return [first.toUpperCase(),...other].join(' ')
// }

// const transform = function(str,fn){
//     console.log(`${str}`)
//     console.log(`${fn(str)}`)
//     console.log(`${fn.name}`)
// }

// transform('ismail is the best',y)
// transform('ismail is the best',x)

// const calc = bill=>bill>=50 && bill<=300? bill*0.15:bill*0.2
// const arr = [22,295,176,440,37,105,10,1100,86,52]
// const tips = [];
// const total = [];

// for (let i = 0;i<arr.length;i++)
// {
//     const tip = calc(arr[i])
//     tips.push(tip);
//     total.push(tip + arr[i])
// }
// console.log(arr , tips , total)

// console.log("ismail")


// const x = {
//     key : 'value',
//     first : 'ismail',
//     last : 'tarek',
//     birth : 2000,
//     jobs : ['teacher','developer'],
//     // calc : function (value)
//     // {
//     //     return 200-value;
//     // }
//     // calc : function ()
//     // {
//     //     return 200-this.age;
//     // }
//     calc : function ()
//     {
//         this.age =  this.birth - 1500;
//         //return this
//     }
// }
// console.log(x.key)
// let y = 'age'
// console.log(x[y])
// let z = 's'
// console.log(x['job' + z])

// console.log(x.calc().calc())
// console.log(x)



// const x = ['ismail','jomana','lolo']
// let y = x.push('lina')
// console.log(x)
// console.log(y)
// let z = x.pop()
// console.log(x)
// x.unshift('ahmed')
// console.log(x)
// let a = x.shift()
// console.log(x)
// console.log(a)
// console.log(x.indexOf('lolo'))
// console.log(x.includes('joman'))

// const calc = bill=>bill>=50 && bill<=300? bill*0.15:bill*0.2

// const arr = [calc(125), calc(500) , calc(40)]
// console.log(arr)


// const x = ['ismail','jomana','lolo']
// console.log(x)
// console.log(x.length)
// console.log(x[2])
// x[2] = "nigga"
// console.log(x)

// const y = ['safia',x,66]
// console.log(y)





// function logger(app,org)
// {
//     const juice = `making juice using ${app} and ${org}`
//     return juice;
// }

// let y = function (value)
// {
//     return 20-value;
// }

// let x = value => 20-value
// let y = x(5)
// console.log(y)

// let x = (x1,x2)=>
// {
//     let age = 26 - x1
//     let year = x2
//     return `${age} year old in ${x2}`
// }

// console.log(x(2,2000))


// let x = logger(5 , 5)
// console.log(x)

// let a = y(10)
// console.log(a)


// let x = 'ismail is here'
// if (x === 'ismail is here')
//     alert("ismail is here");

// let Name = "jomana";
// let pi = 3.14;

// console.log(Name);
// console.log(pi)


// let y = 30
// console.log(y)
// const age = 90
// // age = 80
// console.log(age)

// var h = "nigga"
// console.log(h)

// const now = 30
// let x = now - 50
// let y = now -10

// console.log(x , y)

// console.log(x ** 2 , y / 10)
// let z = "ismail" + ' ' + "is here"
// console.log(z)

// let a = x++
// console.log(a )

// let x,y;
// x = y = 30+50-10;
// let a = 30 , b = 50
// let average = (a + b) / 10
// console.log((x+10) > (y+50))
// console.log(average)

// const _name = "ismail"
// const age = 25
// const job = "developer"

// let x = "I'am " + _name + " work as " + job + " am " + age + " years old"
// let y = `I'am ${_name} work as ${job} am ${age -10} years old`
// console.log(y + '\t fk off')

// const x = Number(23)
// console.log(typeof x)


// let y
// if(y)
//     console.log("go fk ur self")
// else
//     console.log("go fk ur self2222")

// const x = prompt("3aiz eh yaba")
// console.log(x)

// if (Number(x) === 25)
// {
//     console.log("this is my age")
// }
// else
// {
//     console.log("get out")
// }

// const day = "sunday"
// switch (day)
// {
//     case "sunday":
//         console.log("today is sunday")
//         break;
//     case "monday":
//         console.log("today is monday")
//         break;
//     default:
//         console.log("nahhhh")
//         break;
// }

// console.log(`can i ${5>20?'kill myself':'fuck myself'}`)








