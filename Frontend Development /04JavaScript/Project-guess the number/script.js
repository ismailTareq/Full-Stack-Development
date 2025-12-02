'use strict';

// console.log(document.querySelector('.message').textContent);
// document.querySelector('.message').textContent = "nigga";
// console.log(document.querySelector('.message').textContent);

// document.querySelector('.number').textContent = 13;
// document.querySelector('.score').textContent = 50;
// document.querySelector('.guess').value = 13;

let secret_number = Math.trunc(Math.random()*20)+1

let highScore = 0

// console.log(document.querySelector('.number').textContent)

let trails = Number(document.querySelector('.score').textContent)
console.log(trails , typeof trails)

const x = function(){
    let guess = Number(document.querySelector('.guess').value)
    console.log(guess, typeof guess)
    if (!guess)
    {
        document.querySelector('.message').textContent = "no number provided"
    }
    else if (guess === secret_number)
    {
        document.querySelector('body').style.backgroundColor = '#60b347';
        // document.querySelector('header').style.borderBottom = '50px solid';
        document.querySelector('.number').textContent = secret_number
        document.querySelector('.number').style.width = '30rem';
        document.querySelector('.message').textContent = "Correct Number! yeeeb"
        highScore += trails
        document.querySelector('.highscore').textContent = highScore

    }
    else if (guess !== secret_number)
    {
        if (trails > 1)
        {
            document.querySelector('.message').textContent = guess > secret_number? "guess a lower number!" : "guess a higher number!" 
            trails--
            document.querySelector('.score').textContent = trails
        }
        else
        {
            document.querySelector('.message').textContent = "you lost bitch"
        }
    }
    // else if (guess > secret_number)
    // {
    //     if (trails > 1)
    //     {
    //         document.querySelector('.message').textContent = "guess a lower number!"
    //         trails--
    //         document.querySelector('.score').textContent = trails
    //     }
    //     else
    //     {
    //         document.querySelector('.message').textContent = "you lost bitch"
    //     }
        
    // }
    // else if (guess < secret_number)
    // {
    //     if (trails > 1)
    //     {
    //         document.querySelector('.message').textContent = "guess a higher number!"
    //         trails--
    //         document.querySelector('.score').textContent = trails
    //     }
    //     else
    //     {
    //         document.querySelector('.message').textContent = "you lost bitch"
    //     }

    // }
}
let y = function(){
    trails = 20
    secret_number = Math.trunc(Math.random()*20)+1
    document.querySelector('.message').textContent = "start guessing ..."
    document.querySelector('.score').textContent = trails
    document.querySelector('.number').textContent = '?'
    document.querySelector('.guess').value = ''
    document.querySelector('body').style.backgroundColor = '#222';
    document.querySelector('.number').style.width = '15rem';
}

document.querySelector('.check').addEventListener('click',x)
document.querySelector('.again').addEventListener('click',y)





