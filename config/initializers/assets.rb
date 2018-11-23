# Be sure to restart your server when you modify this file.

# Version of your assets, change this if you want to expire all your assets.
Rails.application.config.assets.version = '1.0'

# Add additional assets to the asset load path.
# Rails.application.config.assets.paths << Emoji.images_path
# Add Yarn node_modules folder to the asset load path.
Rails.application.config.assets.paths << Rails.root.join('node_modules')

# Precompile additional assets.
# application.js, application.css, and all non-JS/CSS in the app/assets
# folder are already added.

#Stylesheet
Rails.application.config.assets.precompile += %w( home/style.css )
Rails.application.config.assets.precompile += %w( home/nivo-lightbox/default.css )
Rails.application.config.assets.precompile += %w( home/nivo-lightbox/nivo-lightbox.css )
Rails.application.config.assets.precompile += %w( home/bootstrap.css )
Rails.application.config.assets.precompile += %w( home/font-awesome/css/font-awesome.css )

#Javascript
Rails.application.config.assets.precompile += %w( home/bootstrap.js )
Rails.application.config.assets.precompile += %w( home/jquery.1.11.1.js )
Rails.application.config.assets.precompile += %w( home/SmoothScroll.js )
Rails.application.config.assets.precompile += %w( home/nivo-lightbox.js )
Rails.application.config.assets.precompile += %w( home/jqBootstrapValidation.js )
Rails.application.config.assets.precompile += %w( home/contact_me.js )
Rails.application.config.assets.precompile += %w( home/main.js )



