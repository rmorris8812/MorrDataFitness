export class FoodDto {
	id: number;
	name: string;
	calories: number;
	carbs: number;
	fat: number;
	sodium: number;
	fiber: number;

	constructor() {
		this.id = 0;
		this.name = '';
		this.calories = 0;
		this.carbs = 0;
		this.fat = 0;
		this.sodium = 0;
		this.fiber = 0;
	}
}