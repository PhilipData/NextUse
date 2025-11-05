import { Profile } from "./profile";

export interface Rating {
    id: number;
    name: string;
    score: number;
    fromProfileId: number;
    fromProfile: Profile | null;
    toProfileId: number;
    toProfile: Profile | null;
    createdAt: string;
}