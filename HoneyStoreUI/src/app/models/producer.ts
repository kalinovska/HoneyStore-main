export class Producer {
    id: number | undefined;
    name: string | undefined;

    constructor(name: string = '', id: number = 0) {
        this.id = id;
        this.name = name;        
    }
}