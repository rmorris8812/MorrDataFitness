import { IngredientDto } from './ingredient.dto'

export class RecipeDto {
  id: number;
  name : string;
  description : string;
  createDate : string;
  updateDate : string;
  lastAccessDate : string;
  servings: number;
  servingSize : number;
  calories : number;
  instructions : string;
  ingredients : IngredientDto[];
}
