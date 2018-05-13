import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import moment from 'moment';

class TimeRegistrationForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            timeRegistration: {
                date: '',
                hoursWorked: 0
            },
            projects: [],
            errors: {}
        };
    }

    componentDidMount() {
        this.props.fetchAllProjects()
            .then(response => {
                const projects = response.content;
                this.setState({ projects: projects });

                if (this.props.match.path === '/timeregistrations/edit/:id') {
                    this.props.fetchTimeRegistration(this.props.match.params.id)
                        .then(response => {
                            let editTimeRegistration = response.content;
                            editTimeRegistration.date = moment(editTimeRegistration.date).format('YYYY-MM-DD');
                            this.setState({ timeRegistration: editTimeRegistration });
                        });
                } else {
                    let newTimeRegistration = {
                        projectId: this.state.projects[0].id,
                        projectName: '',
                        customerName: '',
                        date: moment().format('YYYY-MM-DD'),
                        hoursWorked: 8
                    }

                    this.setState({ timeRegistration: newTimeRegistration });
                }
            });
    }

    handleSubmit(event) {
        event.preventDefault();
        this.saveTimeRegistration(this.state.timeRegistration);
    }

    handleInputChange(event) {
        event.preventDefault();
        const target = event.target;

        let value;
        if (target.type.indexOf('select') === 0) {
            value = target.options[target.selectedIndex].value;
        } else {
            value = target.value;
        }
        const name = target.name;
        let timeRegistrationUpdates = {
            [name]: value
        };

        this.setState({
            timeRegistration: Object.assign(this.state.timeRegistration, timeRegistrationUpdates)
        });
    }

    saveTimeRegistration(timeRegistration) {
        this.setState({ errors: {} });
        this.props.saveTimeRegistration(timeRegistration).then((response) => {
            if (!response.is_error) {
                this.props.history.push('/timeregistrations');
            } else {
                this.setState({ errors: response.error_content });
            }
        });
    }

    render() {
        return (
            <fieldset className="form-group">
                <legend>{this.state.timeRegistration.id ? "Edit Time Registration" : "New Time Registration"}</legend>
                <form onSubmit={(e) => this.handleSubmit(e)}>
                    <div>
                        <label htmlFor="selProjects" className="form-control-label">Project:</label>
                        <select data-testid="selProjects" id="selProjects" name="projectId" onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" value={this.state.timeRegistration.projectId}>
                            {this.state.projects.map(project =>
                                <option key={project.id} value={project.id}>{project.name} ({project.customerName})</option>
                            )}
                        </select>

                    </div>
                    <div>
                        <label htmlFor="inputDate" className="form-control-label">Date:</label>
                        <input id="inputDate" name="date" onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" value={this.state.timeRegistration.date} type="date" />
                    </div>
                    <div>
                        <label htmlFor="inputHoursWorked" className="form-control-label">Hours Worked:</label>
                        <input id="inputHoursWorked" name="hoursWorked" onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" value={this.state.timeRegistration.hoursWorked} type="number" />
                    </div>
                    <button className="btn btn-primary" type="submit">Save</button>
                    <Link className="btn btn-secondary" to="/timeregistrations">Cancel</Link>
                </form>
            </fieldset>
        );
    }
}

export default TimeRegistrationForm;