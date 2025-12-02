// src/js/controller.js
/* parcel-plugin-url-loader (or Parcel 2 built-in) turns this into a
   public URL at build time */
import * as model from './model.js'
import recipeView from './views/recipeView.js';
import searchView from './views/searchView.js'
import resultView from './views/resultView.js';
import paginationView from './views/paginationView.js';
import bookmarkView from './views/bookmarkView.js';
import addRecipeView from './views/addRecipeView.js';

import 'core-js/stable'
import 'regenerator-runtime/runtime'


const timeout = s =>
  new Promise((_, reject) =>
    setTimeout(() => reject(new Error(`Request took too long! Timeout after ${s} second`)), s * 1000)
  );


const controlRecipes = async function () {
  try {
    const hash = window.location.hash.slice(1)
    // console.log(hash)
    if(!hash)return
    recipeView.renderSpinner()
    // const res = await fetch(
    //   `https://forkify-api.jonas.io/api/v2/recipes/${hash}`
    // );
    // if (!res.ok) throw new Error(`${res.status}`);
    // const { data } = await res.json();
    // const {
    //   id,
    //   title,
    //   publisher,
    //   source_url,
    //   image_url,
    //   servings,
    //   cooking_time,
    //   ingredients,
    // } = data.recipe;
    
    resultView.update(model.getSearchResultPage())
    bookmarkView.update(model.state.bookmarks)

    await model.loadRecipe(hash)
    
    
    recipeView.render(model.state.recipe)

    // const {
    //   id,
    //   title,
    //   publisher,
    //   source_url,
    //   image_url,
    //   servings,
    //   cooking_time,
    //   ingredients,
    // } = model.state.recipe;
    // const html = `
    
    
    
    //   <div class="spinner"></div>

    //   <figure class="recipe__fig">
    //     <img src="${image_url}" alt="${title}" class="recipe__img" />
    //     <h1 class="recipe__title">
    //       <span>${title}</span>
    //     </h1>
    //   </figure>

    //   <div class="recipe__details">
    //     <div class="recipe__info">
    //       <svg class="recipe__info-icon">
    //         <use href="${icons}#icon-clock"></use>
    //       </svg>
    //       <span class="recipe__info-data recipe__info-data--minutes">${cooking_time}</span>
    //       <span class="recipe__info-text">minutes</span>
    //     </div>

    //     <div class="recipe__info">
    //       <svg class="recipe__info-icon">
    //         <use href="${icons}#icon-users"></use>
    //       </svg>
    //       <span class="recipe__info-data recipe__info-data--people">${servings}</span>
    //       <span class="recipe__info-text">servings</span>

    //       <div class="recipe__info-buttons">
    //         <button class="btn--tiny btn--decrease-servings">
    //           <svg><use href="${icons}#icon-minus-circle"></use></svg>
    //         </button>
    //         <button class="btn--tiny btn--increase-servings">
    //           <svg><use href="${icons}#icon-plus-circle"></use></svg>
    //         </button>
    //       </div>
    //     </div>

    //     <div class="recipe__user-generated">
    //       <svg><use href="${icons}#icon-user"></use></svg>
    //     </div>

    //     <button class="btn--round">
    //       <svg><use href="${icons}#icon-bookmark-fill"></use></svg>
    //     </button>
    //   </div>

    //   <div class="recipe__ingredients">
    //     <h2 class="heading--2">Recipe ingredients</h2>
    //     <ul class="recipe__ingredient-list">
    //       ${ingredients
    //         .map(
    //           ing => `
    //           <li class="recipe__ingredient">
    //             <svg class="recipe__icon">
    //               <use href="${icons}#icon-check"></use>
    //             </svg>
    //             <div class="recipe__quantity">${ing.quantity || ''}</div>
    //             <div class="recipe__description">
    //               <span class="recipe__unit">${ing.unit || ''}</span>
    //               ${ing.description}
    //             </div>
    //           </li>`
    //         )
    //         .join('')}
    //     </ul>
    //   </div>

    //   <div class="recipe__directions">
    //     <h2 class="heading--2">How to cook it</h2>
    //     <p class="recipe__directions-text">
    //       This recipe was carefully designed and tested by
    //       <span class="recipe__publisher">${publisher}</span>.
    //       Please check out directions at their website.
    //     </p>
    //     <a class="btn--small recipe__btn" href="${source_url}" target="_blank">
    //       <span>Directions</span>
    //       <svg class="search__icon">
    //         <use href="${icons}#icon-arrow-right"></use>
    //       </svg>
    //     </a>
    //   </div>
    // `;

    // recipeContainer.innerHTML = '';
    // recipeContainer.insertAdjacentHTML('afterbegin', html);
  } catch (err) {
    console.error(err);
    recipeView.renderError()
  }
};

const controlSearch = async function() {
  try{
    resultView.renderSpinner()

    const query = searchView.getQuery()
    if(!query)return;
    await model.searchRecipe(query)
    // console.log(model.state.search.results)
    resultView.render(model.getSearchResultPage())

    paginationView.render(model.state.search)
    }catch(err){
        console.error(err);
        recipeView.renderError()
      } 
}
const controlPangination = async function (goto) {
    resultView.render(model.getSearchResultPage(goto))

    paginationView.render(model.state.search)
}
const controlServing = function(serving){
  model.updateServing(serving)

  recipeView.render(model.state.recipe)
  // recipeView.update(model.state.recipe)
}
const controlBookMarked = function(){
  if(!model.state.recipe.bookmarked)
    model.addBookmark(model.state.recipe)
  else if(model.state.recipe.bookmarked)
    model.deleteBookmark(model.state.recipe.id)
  // console.log(model.state.recipe)
  recipeView.update(model.state.recipe)
  bookmarkView.render(model.state.bookmarks)
}

const controlBookmarks = function(){
  bookmarkView.render(model.state.bookmarks)
}

const controlAddRecipe = async function(newrecipe){
  try {
    addRecipeView.renderSpinner();

    await model.uploadRecipe(newrecipe);
    // console.log(model.state.recipe)
    recipeView.render(model.state.recipe);

    addRecipeView.renderSuccMessages();

    bookmarkView.render(model.state.bookmarks)

    window.history.pushState(null,'',`#${model.state.recipe.id}`)
  } catch (err) {
    console.log(err);
    addRecipeView.renderError(err);
  }
}

// controlSearch()
const init = function(){
  bookmarkView.addHandlerRender(controlBookmarks)
  recipeView.addHandlerRender(controlRecipes);
  recipeView.addHandlerUpdateServings(controlServing);
  recipeView.addHandlerAddMark(controlBookMarked);
  searchView.addHandlerSearch(controlSearch);
  paginationView.addHandleClick(controlPangination);
  addRecipeView.addHandlerUpload(controlAddRecipe)
}
init()


// showRecipe();

// window.addEventListener('hashchange',showRecipe)
