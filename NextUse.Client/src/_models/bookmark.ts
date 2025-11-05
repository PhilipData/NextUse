import { Profile } from "./profile";
import { Product } from "./product";

export interface Bookmark {
    id: number;
    profileId?: number;
    profile?: Profile;
    productId?: number;
    product?: Product;
}// poke
