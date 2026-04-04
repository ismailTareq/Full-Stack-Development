import classes from './Counter.module.css';
import { useSelector } from 'react-redux';

const Counter = () => {
  const counter = useSelector(state => state.counter)

  const toggleCounterHandler = () => {};

  return (
    <main className={classes.counter}>
      <h1>Redux Counter</h1>
      <div className={classes.value}>-- COUNTER VALUE --</div>
      <button onClick={toggleCounterHandler}>{counter}</button>
    </main>
  );
};

export default Counter;
