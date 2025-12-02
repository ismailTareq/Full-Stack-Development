import { addtocart, price, quantity } from './shoppingCart.js'
console.log('imporitingg')

addtocart('bread',9)

console.log(price,quantity)

const getLastPost = async function(){
    const res = await fetch('https://jsonplaceholder.typicode.com/posts')
    const data = await res.json()
    return {title: data.at(-1).title,text: data.at(-1).body}
}

const x = await getLastPost()
console.log(x)
// x.then(res => console.log(res))

import CleanDeep from './node_modules/lodash-es/cloneDeep.js'

const state = {
    cart: 
    [
        {product:'bread',quantity:5},
        {product:'pizza',quantity:6}
    ],
    user:{loggin:true}
}
let y = Object.assign({},state)
state.user.loggin = false
console.log(x)
