'use strict';

// Data needed for a later exercise
const flights =
  '_Delayed_Departure;fao93766109;txl2133758440;11:25+_Arrival;bru0943384722;fao93766109;11:45+_Delayed_Arrival;hel7439299980;fao93766109;12:05+_Departure;fao93766109;lis2323639855;12:30';

const italianFoods = new Set([
  'pasta',
  'gnocchi',
  'tomatoes',
  'olive oil',
  'garlic',
  'basil',
]);

const mexicanFoods = new Set([
  'tortillas',
  'beans',
  'rice',
  'tomatoes',
  'avocado',
  'garlic',
]);

const week = ['sun','mon','tue']

const hours = {
    [week[1]]: {
      open: 12,
      close: 22,
    },
    fri: {
      open: 11,
      close: 23,
    },
    sat: {
      open: 0, 
      close: 24,
    },
};

const restaurant = {
  name1: 'Classico Italiano',
  location: 'Via Angelo Tavanti 23, Firenze, Italy',
  categories: ['Italian', 'Pizzeria', 'Vegetarian', 'Organic'],
  starterMenu: ['Focaccia', 'Bruschetta', 'Garlic Bread', 'Caprese Salad'],
  mainMenu: ['Pizza', 'Pasta', 'Risotto'],

  hours,

  order : function(Sindex,Mindex)
  {
    return([this.starterMenu[Sindex] , this.mainMenu[Mindex]]);
  }
};

const x = new Map([
  ['question','what are you learning'],
  [1,'C'],
  [2,'javascript'],
  ['correct',2],
  [true , 'correct'],
  [false , 'false'],
]);

// console.log(x)

// for (const [i,j] of x)
// {
//   if (typeof i === 'number')
//     console.log(i , j)
// }

// const answer = Number(prompt('your answer'))
// console.log(x.get(x.get('correct') === answer))

console.log([...x])
console.log(x.keys())


// const x = new Map()
// x.set('name',"ismail")
// x.set(1,"jomana").set('array',['ismail','jomana','lila']).set('open',11)
// .set('close',23)
// .set(true,'we are open')
// .set(false,'we are close')

// const time = 21
// x.get(time > x.get('open') && time < x.get('closes'))
// console.log(x.get(time > x.get('open') && time < x.get('closes')))


// const x = [{name : 'ismail',email : '@gmail'}]
// console.log(x[1]?.name ?? 'empty')

// for (const i of Object.keys(hours))
//   console.log(i)

// const y = Object.keys(hours)
// console.log(y)
// const x = restaurant.hours.fri.nig
// console.log(restaurant.hours.thur?.open)
// console.log(restaurant.order?.(0,1))
// let y = undefined ?? 2
// console.log(y)
// const [x,y,...others] = [
//   ...restaurant.mainMenu,
//   ...restaurant.starterMenu
// ]
// console.log(x , y)
// console.log("---------------")
// console.log(others)

// const {sat,...ax} = restaurant.openingHours
// console.log(ax)

// const menu = [...restaurant.starterMenu,...restaurant.mainMenu]

// for (const x of menu.entries())
//   console.log(x[0] , x[1])

// const ingrediants = [
//   prompt("ingrediants 1"),
//   prompt("ingrediants 2"),
//   prompt("ingrediants 3"),
// ]

// console.log(ingrediants)
// const x = {...restaurant,Founder:'ismail'}
// console.log(x)
// console.log(restaurant)
// x.name1 = 'niggas restorunt'
// console.log(x.name1)
// console.log(restaurant.name1)


// const {fri: {open,close}} = restaurant.openingHours
// console.log(open,close)
// const menu = [...restaurant.mainMenu,...restaurant.starterMenu]
// console.log(menu)

// const {name1 , openingHours , categories} = restaurant
// console.log(name1,openingHours,categories)
// const {name1 : n , openingHours : o , categories : c} = restaurant
// console.log(n,o,c)
// const { menu = [] , starterMenu : s = []} = restaurant
// console.log(menu , s)
// let a = 1;
// let b = 2;
// const i = {a: 3, b: 4, c: 5};

// ({a, b} = i);   // <-- note the parentheses
// console.log(a, b); // 3 4



// const arr = [3,2,5]
// const [x,y,z] = arr
// console.log(x,y,z)

// let [main , , sec] = restaurant.categories;
// console.log(main,sec)

// const [starter , mains] = restaurant.order(2,0)
// console.log(mains,starter)

// const a = [2,5,[3,6]]
// const [b,c,[d,e],f=1] = a
// console.log(b,c,d,e,f)