'use strict';

///////////////////////////////////////
// Modal window

const modal = document.querySelector('.modal');
const overlay = document.querySelector('.overlay');
const btnCloseModal = document.querySelector('.btn--close-modal');
const btnsOpenModal = document.querySelectorAll('.btn--show-modal');

const openModal = function (e) {
  e.preventDefault();
  modal.classList.remove('hidden');
  overlay.classList.remove('hidden');
};

const closeModal = function () {
  modal.classList.add('hidden');
  overlay.classList.add('hidden');
};

btnsOpenModal.forEach(btn => btn.addEventListener('click', openModal));

btnCloseModal.addEventListener('click', closeModal);
overlay.addEventListener('click', closeModal);

document.addEventListener('keydown', function (e) {
  if (e.key === 'Escape' && !modal.classList.contains('hidden')) {
    closeModal();
  }
});


////////////////
// Cookie message
const header = document.querySelector('.header');
const message = document.createElement('div');
message.classList.add('cookie-message');
message.innerHTML =
  'We use cookies for improved functionality and analytics. <button class="btn btn--close-cookie">Got it!</button>';
// header.prepend(message);
header.append(message);

// Delete cookie message
document
  .querySelector('.btn--close-cookie')
  .addEventListener('click', function () {
    message.remove();
  });
// style
message.style.backgroundColor = '#37383d';
message.style.width = '120%';
console.log(getComputedStyle(message).height);
console.log(Number.parseFloat((getComputedStyle(message).height), 10))
message.style.height = Number.parseFloat((getComputedStyle(message).height), 10)+ 30 + 'px';

document.documentElement.style.setProperty('--color-primary', 'red');

let logo = document.querySelector('.nav__logo');
console.log(logo.alt);
console.log(logo.src);
console.log(logo.className);

const btnScrollTo = document.querySelector('.btn--scroll-to');
const section1 = document.querySelector('#section--1');

btnScrollTo.addEventListener('click', function (e) {
  const s1coords = section1.getBoundingClientRect();
  console.log(s1coords);
  console.log(e.target.getBoundingClientRect());
  console.log('Current scroll (X/Y)', window.pageXOffset, window.pageYOffset);
  console.log(
    'height/width viewport',
    document.documentElement.clientHeight,
    document.documentElement.clientWidth
  );
  section1.scrollIntoView({ behavior: 'smooth' });
  // window.scrollTo({
  //   left: s1coords.left + window.pageXOffset,
  //   top: s1coords.top + window.pageYOffset,
  //   behavior: 'smooth',
  // });

});
const z = document.querySelector('h1');
const y = function (e) {
  alert('addEventListener: Great! You are reading the heading :D');
  // z.removeEventListener('mouseenter', arguments.callee);
}
z.addEventListener('mouseenter', y);

setTimeout(()=>{
  z.removeEventListener('mouseenter', y);
},3000)

const randomInt = (min, max) =>
  Math.floor(Math.random() * (max - min + 1) + min);

const randomColor = () =>
  `rgb(${randomInt(0, 255)},${randomInt(0, 255)},${randomInt(0, 255)})`;

// document.querySelectorAll('.nav__link').forEach(function(el){
//   el.addEventListener('click', function (e) {
//     e.preventDefault();
//     const link = this.getAttribute('href');
//     console.log(link);
//     document.getElementById(link.slice(1)).scrollIntoView({ behavior: 'smooth' });
    
//   });
// });

document.querySelector('.nav__links').addEventListener('click', function (e) {
  console.log(e.target);
  e.preventDefault();
  if (e.target.classList.contains('nav__link')) {
    const link = e.target.getAttribute('href');
    console.log(link.slice(1));
    document.getElementById(link.slice(1)).scrollIntoView({ behavior: 'smooth' });
  }
});

const tab = document.querySelectorAll('.operations__tab');
const tabContainer = document.querySelector('.operations__tab-container');
const tabContent = document.querySelectorAll('.operations__content');

tabContainer.addEventListener('click', function (e) {
  const clicked = e.target.closest('.operations__tab');
  console.log(clicked);
  if (!clicked) return;
  tab.forEach(t => t.classList.remove('operations__tab--active'));
  tabContent.forEach(t => {
    console.log(t);
    t.classList.remove('operations__content--active');
  });
  clicked.classList.add('operations__tab--active');
  console.log(clicked.dataset.tab);
  document.querySelector(`.operations__content--${clicked.dataset.tab}`).classList.add('operations__content--active');
});

const nav = document.querySelector('.nav');

const hadleHover = function(e){
  if(e.target.classList.contains('nav__link')){
    const link = e.target;
    console.log(link);
    console.log(this);
    const siblings = link.closest('.nav').querySelectorAll('.nav__link');
    console.log(siblings);
    const logo = link.closest('.nav').querySelector('img'); 
    siblings.forEach(el => {
      if (el !== link) el.style.opacity = this;
    });
    logo.style.opacity = this;
  }
};

nav.addEventListener('mouseover', hadleHover.bind(0.5));
nav.addEventListener('mouseout', hadleHover.bind(1));

const header1 = document.querySelector('.header');
const navHeight = nav.getBoundingClientRect().height;
const stickyNav = function(entries){
  const entry = entries[0];
  if(!entry.isIntersecting) nav.classList.add('sticky');
  else nav.classList.remove('sticky');
}
const headeroserver = new IntersectionObserver(stickyNav, {
  root : null,
  threshold : 0,
  rootMargin : `-${navHeight}px`,
});

headeroserver.observe(header1);
const allSections = document.querySelectorAll('.section');
const sectionObs = function(entries, observer){
  entries.forEach(entry => {
    if(entry.isIntersecting){
      entry.target.classList.remove('section--hidden');
      observer.unobserve(entry.target);
    }
  });
  // const entry = entries[0];
  // if(entry.isIntersecting){
  //   entry.target.classList.remove('section--hidden');
  //   observer.unobserve(entry.target);
  // }
}
const sectionobserver = new IntersectionObserver(sectionObs, {
  root : null,
  threshold : 0.15,
});
allSections.forEach(function(section){
  sectionobserver.observe(section);
  // section.classList.add('section--hidden');
});

const imgTargets = document.querySelectorAll('img[data-src]');
const loadimg = function(entries, observer){
  const entry = entries[0];
  if(!entry.isIntersecting) return;
  entry.target.src = entry.target.dataset.src;
  entry.target.addEventListener('load', function(){
    entry.target.classList.remove('lazy-img');
  });
  observer.unobserve(entry.target);
}
const imgObserver = new IntersectionObserver(loadimg,{});

imgTargets.forEach(img => imgObserver.observe(img));

const slides = document.querySelectorAll('.slide');
const slider = document.querySelector('.slider');
const btnLeft = document.querySelector('.slider__btn--left');
const btnRight = document.querySelector('.slider__btn--right');
let curSlide = 0;
const maxSlide = slides.length;
// slides.forEach((s, i) => (s.style.transform = `translateX(${100 * i}%)`));
// slider.style.transform = 'scale(1)';
// slider.style.overflow = 'visible';

const goToSlide = function(slide){
  slides.forEach((s, i) => {s.style.transform = `translateX(${100 * (i - slide)}%)`});
}
const activateDot = function(slide){
  document.querySelectorAll('.dots__dot').forEach(dot => dot.classList.remove('dots__dot--active'));
  document.querySelector(`.dots__dot[data-slide="${slide}"]`).classList.add('dots__dot--active');
}

btnRight.addEventListener('click', function(){
  if(curSlide === maxSlide - 1){
    curSlide = 0;
  }
  else{
    curSlide++;
  }
  goToSlide(curSlide);
  activateDot(curSlide);
});

btnLeft.addEventListener('click', function(){
  if(curSlide === 0){
    curSlide = maxSlide - 1;
  }
  else{
    curSlide--;
  }
  goToSlide(curSlide);
  activateDot(curSlide);
});


const dotContainer = document.querySelector('.dots');
const createDots = function(){
  slides.forEach(function(_, i){

    dotContainer.insertAdjacentHTML('beforeend', `<button class="dots__dot" data-slide="${i}"></button>`);
  });
}
createDots();


dotContainer.addEventListener('click', function(e){
  if(e.target.classList.contains('dots__dot')){
    const slide = e.target.dataset.slide;
    goToSlide(slide);
    activateDot(slide);
  }
});




// const initcoords = section1.getBoundingClientRect();
// console.log(initcoords);

// window.addEventListener('scroll', function(){
//   if(window.scrollY > initcoords.top) nav.classList.add('sticky');
//   else nav.classList.remove('sticky');
// });

// const h = document.querySelector('h1');
// console.log(h.querySelectorAll('.highlight'));
// console.log(h.childNodes);
// console.log(h.children);
// console.log(h.firstElementChild);
// console.log(h.lastElementChild);

// console.log(h.parentNode);

// document.querySelector('.nav__links').addEventListener('click', function (e) {
//   this.style.backgroundColor = randomColor();
//   console.log('LINK', e.target, e.currentTarget);
//   // e.stopPropagation();
// });

// document.querySelector('.nav__link').addEventListener('click', function (e) {
//   this.style.backgroundColor = randomColor();
//   console.log('LINK', e.target, e.currentTarget);
//   // e.stopPropagation();
// });
