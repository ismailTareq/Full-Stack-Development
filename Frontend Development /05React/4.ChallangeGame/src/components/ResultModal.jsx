export default function ResultModal({ ref,result, targetTime, remainingtime, onReset }) {
    const userlost = remainingtime <=0
    const remainingMill = (remainingtime / 1000).toFixed(2)
    const score = Math.round((1 - remainingtime / (targetTime * 1000)) * 100)
    
    return (
    <dialog ref={ref} className="result-modal">
      {userlost && <h2>You lost</h2>}
      {!userlost && <h2>Your Score bitch: {score}</h2>}
      <p>
        The target time was <strong>{targetTime} seconds.</strong>
      </p>
      <p>
        You stopped the timer with <strong>{remainingMill} seconds left.</strong>
      </p>
      <form method="dialog" onSubmit={onReset}>
        <button>Close</button>
      </form>
    </dialog>
  );
}
