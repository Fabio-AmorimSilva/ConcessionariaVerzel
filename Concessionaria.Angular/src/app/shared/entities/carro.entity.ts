import { Register } from './register.entity';
import { Enumeration } from './enumeration.entity';

export class Carro extends Register{
  nome!: string;
  marca!: string;
  modelo!: string;
  valor!: number;
  foto?: string;
  idTipoCarro!: number;
  tipoCarro!: Enumeration;
}
