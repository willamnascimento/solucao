import { EquipamentSpecifications } from "./equipamentSpecifications";

export interface Equipament {
    id: string;
    name: string;
    active: boolean;
    equipamentSpecifications: EquipamentSpecifications[];
  }