import { Profile } from "./profile";
import { Product } from "./product";

export interface Address {
    id: number;
    country: string;
    city: string;
    postalCode: number;
    street?: string;
    houseNumber?: string;

    profileId?: number;
    profile?: Profile;
    productId?: number;
    product?: Product;
}