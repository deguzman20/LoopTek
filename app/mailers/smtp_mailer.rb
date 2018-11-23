class SmtpMailer < ApplicationMailer
  def send_email(user,subj)
    @user = user
    mail(to: @user.email, subject: subj.to_s)
  end
end
