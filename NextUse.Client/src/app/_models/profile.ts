//import { User } from "./user";
// TODO how do we handle user? we need a user but unsure of how much of the user we need... Identity user 
//Authentication - Email, Password etc ?


import { Address } from "./address";
import { Rating } from "./rating";
import { Bookmark } from "./bookmark";
import { UserComment } from "./comment";
import { Message } from "./message";
import { Product } from "./product";
import { Cart } from "./cart";

export interface Profile {
    id: number;
    name: string;
    averageRating: number;
    ratingAmount: number;
    addressId: number;
    //user: User | null;
    address: Address | null;
    ratings: Rating[];
    bookmarks: Bookmark[];
    comments?: UserComment[] | null;
    messages?: Message[] | null;
    products?: Product[] | null;
     carts: Cart[];
     isBlocked: boolean;

}