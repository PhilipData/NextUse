//import { User } from "./user";
//TODO Hvad gør vi med user? vi skal have en, vi ved ikke helt hvor meget af useren vi skal bruge? Identity user
//Authentication - Email, Password etc ?
import { Address } from "./address";
import { Rating } from "./rating";
import { Bookmark } from "./bookmark";
import { UserComment } from "./comment";
import { Message } from "./message";
import { Product } from "./product";

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
}