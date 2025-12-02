console.log('exporing module')
const cost = 10
const arr = []


export const addtocart = function(p,q){
    arr.push({p,q})
    console.log(`${p} and ${q} are add`)
}

const price = 20.36
const quantity = 20

export{ price, quantity }