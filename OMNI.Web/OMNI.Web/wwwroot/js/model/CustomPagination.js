class GenerateHTMLElement {

    generateHTML = () => {
        throw new Error("Method 'say(generateHTML)' must be implemented.");
    }
}

class Advocate extends GenerateHTMLElement{
    constructor({
        name,
        status = true,
        photoUrl = `https://bprpedungan.com/wp-content/uploads/2017/08/Person-placeholder-300x300.jpg`,
        category,
        court,
        phoneNumber =`-`,
        desc,
        education,
        experienceYear,
        buttons
    }) {
        super();
        this.name = name;
        this.status = status;
        this.photoUrl = photoUrl;
        this.category = category
        this.court = court;
        this.phoneNumber = phoneNumber;
        this.desc = desc;
        this.education = education;
        this.experienceYear = experienceYear;
        this.buttons = buttons;
    }

    generateHTML = () => {
        return `
            <div class="card p-3 mb-2">
                <div class="row no-gutters">
                    <div class="col-sm-2 text-muted d-flex flex-column justify-content-start align-items-start p-2">
                        <img class="rounded-circle img-fluid p-2" src="${this.photoUrl}" />
                        <br>
                        <div class="border-faded mb-2 border-top-0 border-left-0 border-right-0">
                            <div class="card-text">
                                <h6 class="fw-700"><i class="fal fa-crown mr-1"></i> Rating</h6>
                            </div>
                            <h1>
                                <span style="color:blue">
                                    &starf;
                                    &starf;
                                    &starf;
                                    &starf;
                                </span>
                                    &star;
                            </h1>
                        </div>
                        <div class="border-faded mb-2 border-top-0 border-left-0 border-right-0">
                            <div class="card-text">
                                <h6 class="fw-700"><i class="fal fa-gavel mr-1"></i> Category</h6>
                            </div>
                            <h6>
                                ${this.category}
                            </h6>
                        </div>
                        <div class="border-faded mb-2 border-top-0 border-left-0 border-right-0">
                            <div class="card-text">
                                <h6 class="fw-700"><i class="fal fa-mobile mr-1"></i> Phone Number</h6>
                            </div>
                            <div class="card-text">
                                <h6>${this.phoneNumber}</h6>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-10">
                        <div class="card-body">
                            <h3 class="card-title fw-700">${this.name}<br><span class="badge ${this.status ? `badge-success` : `badge-danger`}">${this.status ? `Active`:`Inactive`}</span></h3>
                            <p class="card-text text-wrap">
                                ${this.desc}
                            </p>
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item"> <strong>${this.education}</strong></li>
                            <li class="list-group-item"><strong>${this.experienceYear}</strong><br></li>
                            <li class="list-group-item">
                            </li>
                        </ul>
                        <div class="card-body">
                                <strong>
                                    <div class="card-text">
                                        <h6 class="fw-700"><i class="fal fa-balance-scale mr-1"></i> Pengadilan</h6>
                                    </div>
                                    <h6>
                                        ${this.court}
                                    </h6>
                                </strong>
                        </div>
                        <div class="card-footer">
                            ${this.buttons.join(" ")}
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}