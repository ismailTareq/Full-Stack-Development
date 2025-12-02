'use strict';

const score0 = document.getElementById('score--0')
const score1 = document.getElementById('score--1')
const player0 = document.querySelector('.player--0')
const player1 = document.querySelector('.player--1')
const current0 = document.getElementById('current--0')
const current1 = document.getElementById('current--1')
const dice = document.querySelector('.dice')
const btnNew = document.querySelector('.btn--new')
const btnRoll = document.querySelector('.btn--roll')
const btnHold = document.querySelector('.btn--hold')

let currentScore = 0
let score = [0,0]
let active = 0
let playing = true



score0.textContent = 0
score1.textContent = 0
current1.textContent = 
dice.classList.add('hidden')

let switchPlayer = function()
{
    document.getElementById(`current--${active}`).textContent = 0 
    currentScore = 0
    active = active?0:1
    player0.classList.toggle('player--active')
    player1.classList.toggle('player--active')
}

let x = function(){
    if(playing)
    {
        const d = Math.trunc(Math.random()*6)+1
        console.log(d)
        dice.classList.remove('hidden')
        dice.src = `dice-${d}.png`
        if (d !== 1)
        {
            currentScore += d
            document.getElementById(`current--${active}`).textContent = currentScore
            // current0.textContent = currentScore
        }
        else
        {
            switchPlayer()
        }
    }  
}

let y = function()
{
    if(playing)
    {
        score[active] += currentScore
        document.getElementById(`score--${active}`).textContent = score[active]
        if (score[active] >= 20)
        {
            playing = false
            dice.classList.add('hidden')
            document.querySelector(`.player--${active}`).classList.add('player--winner')
            document.querySelector(`.player--${active}`).classList.remove('player--active')
        }
        else
        {
            switchPlayer()
        }
    }
}

let z = function()
{
    score0.textContent = 0
    score1.textContent = 0
    current0.textContent = 0
    current1.textContent = 0
    player0.classList.remove('player--winner')
    player1.classList.remove('player--winner')
    player0.classList.add('player--active')
    player1.classList.remove('player--active')
    dice.classList.add('hidden')
    currentScore = 0
    score = [0,0]
    active = 0
    playing = true
}

btnRoll.addEventListener('click',x)
btnHold.addEventListener('click',y)
btnNew.addEventListener('click',z)






