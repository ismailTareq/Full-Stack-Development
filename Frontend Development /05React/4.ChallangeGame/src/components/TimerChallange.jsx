import { useState, useRef } from "react"
import ResultModal from "./ResultModal"

export default function TimerChallange({title , targetTime}){
    const timer = useRef()
    const dialog = useRef()

    // const [timerStarted,settimerStarted] = useState(false)
    // const [timerexpired,settimerexpired] = useState(false)

    const [timeRemaining,settimeRemaining] = useState(targetTime * 1000)
    const timerActive = timeRemaining > 0 && timeRemaining < targetTime * 1000

    if (timeRemaining <= 0){
        clearInterval(timer.current)
        // settimeRemaining(targetTime * 1000)
        dialog.current.showModal()
    }

    function handlestart(){
        timer.current = setInterval(() => {
            settimeRemaining(prevTime => prevTime - 10)
            // settimerexpired(true);
            // dialog.current.showModal()
        }, 10)
    }

    function handlerest(){
        settimeRemaining(targetTime * 1000)
    }

    function handlestop(){
        dialog.current.showModal()
         clearInterval(timer.current)
    }
    
    return (
        <>
            <ResultModal ref={dialog} targetTime = {targetTime} remainingtime = {timeRemaining} onReset={handlerest}/>
            <section className="challenge">
                <h2>{title}</h2>
                <p className="challenge-time">
                    {targetTime}second{targetTime > 1 ? 's':''}
                </p>
                <p>
                    <button onClick={timerActive ? handlestop : handlestart}>
                        {timerActive ? 'stop' : 'Start'} challenge
                    </button>
                </p>
                <p className={timerActive ? 'active' : undefined}>
                    {timerActive ? 'Time is running...' : 'Timer inactive'}
                </p>
            </section>
        </>
    )
}