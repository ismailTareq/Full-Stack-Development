'use strict';

// prettier-ignore

const form = document.querySelector('.form');
const containerWorkouts = document.querySelector('.workouts');
const inputType = document.querySelector('.form__input--type');
const inputDistance = document.querySelector('.form__input--distance');
const inputDuration = document.querySelector('.form__input--duration');
const inputCadence = document.querySelector('.form__input--cadence');
const inputElevation = document.querySelector('.form__input--elevation');
let map, mapEvent;
///////////////////////////////////////
class Workout{
    date = new Date();
    id = (Date.now() + '').slice(-10);
    constructor(coords,distance,duration){
        this.coords = coords; // [lat,lng]
        this.distance = distance; // in km
        this.duration = duration; // in min
    }

    Set_description()
    {
        const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        this.description = `${this.type[0].toUpperCase()}${this.type.slice(1)} on 
        ${months[this.date.getMonth()]} ${this.date.getDate()}`
    }
}

class Running extends Workout{
    type = 'running'
    constructor(coords,distance,duration,cadence){
        super(coords,distance,duration);
        this.cadence = cadence;
        this.calcpace();
        this.Set_description();
    }


    calcpace(){
        this.pace = this.duration / this.distance;
        return this.pace;
    }
}

class Cycling extends Workout{
    type = 'cycling'
    constructor(coords,distance,duration,elevationGain){
        super(coords,distance,duration);
        this.elevationGain = elevationGain;
        this.calcSpeed();
        this.Set_description();
    }   

    calcSpeed(){
        this.speed = this.distance / (this.duration / 60);
        return this.speed;
    }
}

class App{
    #map;
    #mapEvent;
    #workouts = [];
    constructor(){
        this._getPosition();
        this._getLocaleStorage();
        form.addEventListener('submit', this._newWorkout.bind(this));
        inputType.addEventListener('change', this._toggleElevationField);
        containerWorkouts.addEventListener('click',this._moveTopopup.bind(this))
    }
    _getPosition(){
        if(navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(this._loadMap.bind(this),function(){
            alert('You are here');
            });
        }
    }
    _loadMap(position){
        let latitude = position.coords.latitude;
        let longitude = position.coords.longitude;
        let coords = [latitude,longitude];
        this.#map = L.map('map').setView(coords, 13);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(this.#map);

        L.marker(coords).addTo(this.#map)
        .bindPopup('A pretty CSS popup.<br> Easily customizable.')
        .openPopup();
        this.#map.on('click', this._showForm.bind(this));
        this.#workouts.forEach(work => {
            this.randerWorkoutMarker(work)
        })
    }
    _showForm(mapEv){
        this.#mapEvent = mapEv;
        form.classList.remove('hidden');
        inputDistance.focus();
    }
    _hideForm(){
        inputCadence.value = inputDuration.value = inputDistance.value = '';
        form.style.display = 'none'
        form.classList.add('hidden')
        setTimeout(() => form.style.display = 'grid',1000)
    }
    _toggleElevationField(){
        inputCadence.closest('.form__row').classList.toggle('form__row--hidden');
        inputElevation.closest('.form__row').classList.toggle('form__row--hidden');
    }

    _newWorkout(e){
        const validInput = (...input) => 
            input.every(inp => Number.isFinite(inp));
        const postiveInput = (...input) => 
            input.every(inp => inp > 0);
        e.preventDefault();
        const distance = +inputDistance.value;
        const duration = +inputDuration.value;
        const type = inputType.value;
        const {lat,lng} = this.#mapEvent.latlng;
        let Workout;
        if (type === 'running'){
            const cadence = +inputCadence.value;
            if(!validInput(distance,duration,cadence) || 
            !postiveInput(distance,duration,cadence)) 
                return alert('enter a postive number dumass')
                Workout = new Running([lat,lng],distance,duration,cadence);
                
        }
        if (type === 'cycling'){
            const elevation = +inputElevation.value;
            if(!validInput(distance,duration,elevation) || 
            !postiveInput(distance,duration)) 
                return alert('enter a postive number dumass')
            Workout = new Cycling([lat,lng],distance,duration,elevation);
        }
        this.#workouts.push(Workout);
        
        console.log(this.#mapEvent);

        this.randerWorkoutMarker(Workout)

        this .randerWorkout(Workout)

        this._hideForm()

        this._setLocalStorage()
        
    }
    _setLocalStorage(){
        localStorage.setItem('workouts',JSON.stringify(this.#workouts))
    }
    
    _moveTopopup(e)
    {
        let works = e.target.closest('.workout')
        if (!works) return
        const w = this.#workouts.find(work => work.id === works.dataset.id)
        this.#map.setView(w.coords,13,{
            animation: true,
            pan:{
                duration:1
            }

        })
    }

    randerWorkout(Workout)
    {
        let html = `
            <li class="workout workout--${Workout.type}" data-id="${Workout.id}">
                <h2 class="workout__title">${Workout.description}</h2>
                <div class="workout__details">
                    <span class="workout__icon">${Workout.type === 'running'?'🏃‍♂️':'🚴‍♀️'}</span>
                    <span class="workout__value">${Workout.distance}</span>
                    <span class="workout__unit">km</span>
                </div>
                <div class="workout__details">
                    <span class="workout__icon">⏱</span>
                    <span class="workout__value">${Workout.duration}</span>
                    <span class="workout__unit">min</span>
                </div>`
        if (Workout.type === 'running')
        {
            html += `
            <div class="workout__details">
                <span class="workout__icon">⚡️</span>
                <span class="workout__value">${Workout.pace.toFixed(1)}</span>
                <span class="workout__unit">min/km</span>
            </div>
                <div class="workout__details">
                <span class="workout__icon">🦶🏼</span>
                <span class="workout__value">${Workout.cadence}</span>
                <span class="workout__unit">spm</span>
            </div>
        </li>
            `
        }
        if (Workout.type === 'cycling'){
            html +=`
                <div class="workout__details">
                    <span class="workout__icon">⚡️</span>
                    <span class="workout__value">${Workout.speed.toFixed(1)}</span>
                    <span class="workout__unit">km/h</span>
                </div>
                <div class="workout__details">
                    <span class="workout__icon">⛰</span>
                    <span class="workout__value">${Workout.elevationGain}</span>
                    <span class="workout__unit">m</span>
                </div>
            </li>
            `
        }
        form.insertAdjacentHTML('afterend',html)
    }

    randerWorkoutMarker(Workout)
    {
        L.marker(Workout.coords)
        .addTo(this.#map)
        .bindPopup(L.popup({
                maxWidth: 250,
                minWidth: 100,
                autoClose: false,
                closeOnClick: false,
                className: `${Workout.type}-popup`
        })).setPopupContent(`${Workout.type === 'running'?'🏃‍♂️':'🚴‍♀️'} ${Workout.description}`).openPopup();
    }
    _getLocaleStorage(){
        const data = JSON.parse(localStorage.getItem('workouts'))
        if(!data) return
        this.#workouts = data
        this.#workouts.forEach(work => {
            this.randerWorkout(work)
        })
    }

    reset(){
        localStorage.removeItem('workouts')
        location.reload
    }
}

const app = new App();






