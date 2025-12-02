'use strict';

const modal = document.querySelector('.modal')
const overlay = document.querySelector('.overlay')
const closeBtn = document.querySelector('.close-modal')
const showBtn = document.querySelectorAll('.show-modal')

let x = function(){
    modal.classList.remove('hidden')
    // modal.style.display = 'block'
    overlay.classList.remove('hidden')
    // overlay.style.display = 'block'
}
let y = function(){
    modal.classList.add('hidden')
    overlay.classList.add('hidden')
}
let z = function(e){
    console.log(e.key)
    if ((e.key === 'Escape') && (!modal.classList.contains('hidden')))
            y()
}

for (let i =0 ; i < showBtn.length ;i++)
    console.log(showBtn[i].addEventListener('click',x))


closeBtn.addEventListener('click',y)
document.addEventListener('keydown',z)








