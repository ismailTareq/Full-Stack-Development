import {async} from 'regenerator-runtime'
import {API_URL,KEY} from './config.js'
import { getJSON, sendJSON } from './helper.js';
export const state = {
  recipe: {},
  search: {
    query:'',
    results:[],
    page: 1,
    resultpage: 10,
  },
  bookmarks: []
};
const createNewRecipeObject = function(data){
  // console.log(data.data)
    const { recipe } = data.data;
    return {
      id: recipe.id,
      title: recipe.title,
      publisher: recipe.publisher,
      source_url: recipe.source_url,
      image_url: recipe.image_url,
      servings: recipe.servings,
      cooking_time: recipe.cooking_time,
      ingredients: recipe.ingredients,
      ...(recipe.key && { key: recipe.key }),
    };
}
export const loadRecipe = async function (id) {
  try {
    
    const res = await fetch(`${API_URL}/${id}?key=${KEY}`);
    if (!res.ok) throw new Error(`${res.status}`);
    const data = await res.json();

    state.recipe = createNewRecipeObject(data)
    
    if(state.bookmarks.some(book => book.id === id))
      state.recipe.bookmarked = true
    else
      state.recipe.bookmarked = false
    // console.log(state.recipe.ingredients)
  } catch (err) {
    console.error(err);
    throw err;
  }
};

export const searchRecipe = async function (query){
    try{
        state.search.query = query
        const res = await fetch(`${API_URL}?search=${query}&key=${KEY}`)
        const {data} = await res.json();
        if (!res.ok) throw new Error(`${res.status}`);
        // console.log(data.recipes)
        state.search.results = data.recipes.map(rec => {
            return {
                id: rec.id,
                title: rec.title,
                publisher: rec.publisher,
                image: rec.image_url,
                ...(rec.key && { key: rec.key }),
            };
        });
        // console.log(state.search.results)
        state.search.page = 1
    }catch(err){
        console.error(err);
        throw err;
    }
}

export const getSearchResultPage = function(page = state.search.page){
    state.search.page = page
    const i = (page -1) * state.search.resultpage;
    const j = (page * state.search.resultpage)
    return state.search.results.slice(i,j)
}

// searchRecipe('pizza')

export const updateServing = function (newServings) {
  state.recipe.ingredients.forEach(ing => {
    ing.quantity = (ing.quantity * newServings) / state.recipe.servings;
  });
  state.recipe.servings = newServings;
};

const localBookmark = function(){
  localStorage.setItem('bookmarks',JSON.stringify(state.bookmarks))
}

export const addBookmark = function(recipe){
  state.bookmarks.push(recipe);

  if (recipe.id === state.recipe.id)
    state.recipe.bookmarked = true
  localBookmark()
}

export const deleteBookmark = function(id){
  const index = state.bookmarks.findIndex(el => el.id === id)
  state.bookmarks.splice(index,1)
  if (id === state.recipe.id)
    state.recipe.bookmarked = false
  localBookmark()
}
export const uploadRecipe = async function(newRecipe){
  try {
    const ingredients = Object.entries(newRecipe)
        .filter(entry => entry[0].startsWith('ingredient') && entry[1] !== '')
        .map(ing => {
          const ingArr = ing[1].split(',').map(el => el.trim());
          if (ingArr.length !== 3)
            throw new Error('Wrong ingredient fromat! Please use the correct format :)');
          const [quantity, unit, description] = ingArr;
          return { quantity: quantity ? +quantity : null, unit, description };
    });
  
    const recipe = {
      title: newRecipe.title,
      source_url: newRecipe.sourceUrl,
      image_url: newRecipe.image,
      publisher: newRecipe.publisher,
      cooking_time: +newRecipe.cookingTime,
      servings: +newRecipe.servings,
      ingredients,
    };
  
      const data = await sendJSON(`${API_URL}?key=${KEY}`, recipe);
      state.recipe = createNewRecipeObject(data);
      console.log(state.recipe)
      addBookmark(state.recipe);
    } catch (err) {
      throw err;
    }
}
  
const init = function(){
  const storage = localStorage.getItem('bookmarks')
  if(storage) state.bookmarks = JSON.parse(storage)
}
init()


const deinit = function(){
  localStorage.clear('bookmarks')
}
// deinit()